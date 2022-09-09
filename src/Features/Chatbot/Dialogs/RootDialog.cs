namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private readonly IAppoinmentBotService _botService;

    public RootDialog(IAppoinmentBotService botService) : base(nameof(RootDialog))
    {
        _botService = botService;

        var waterfallSteps = new WaterfallStep[]
        {
            ShowNameOfPatients,
            ShowNameOfOffices,
            ShowNameOfServices,
            ShowNameOfDentists,
            ShowAppoinmentDate,
            ShowSchedules,
            ShowAppointmentData
        };

        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        AddDialog(new AdaptiveCardPrompt(nameof(AdaptiveCardPrompt), ValidateChoiceSet));
        AddDialog(new TextPrompt(nameof(TextPrompt), ValidateSchedule));
    }

    private async Task<bool> ValidateChoiceSet(PromptValidatorContext<JObject> promptContext, CancellationToken cancellationToken)
        => await Task.FromResult(promptContext.Context.Activity.Value is not null);

    private async Task<bool> ValidateSchedule(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        => await Task.FromResult(promptContext.Context.Activity.Value is not null);

    private async Task<DialogTurnResult> ShowNameOfPatients(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var userProfile = stepContext.CreateUserProfileInstance();
        stepContext.CreateAppoinmentInstance().UserId = userProfile.Id;
        var choicesTask = _botService.GetPatientsAsync(userProfile);
        var cardJsonTask = TemplateCardLoader.LoadPatientCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfOffices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        stepContext.GetAppoinment().PersonId = int.Parse(stepContext.GetValueFromJObject(CardType.PatientId)); 
        var choicesTask = _botService.GetOfficesAsync();
        var cardJsonTask = TemplateCardLoader.LoadOfficeCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfServices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        stepContext.GetAppoinment().OfficeId = int.Parse(stepContext.GetValueFromJObject(CardType.OfficeId));
        var choicesTask = _botService.GetDentalServicesAsync();
        var cardJsonTask = TemplateCardLoader.LoadDentalServiceCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfDentists(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var appoinment = stepContext.GetAppoinment();
        int officeId = appoinment.OfficeId;
        appoinment.GeneralTreatmentId = int.Parse(stepContext.GetValueFromJObject(CardType.DentalServiceId));
        var choicesTask = _botService.GetDentistsByOfficeIdAsync(officeId);
        var cardJsonTask = TemplateCardLoader.LoadDentistCardAsync();
        var choices = await choicesTask;
        var cardJson = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowAppoinmentDate(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        stepContext.GetAppoinment().DentistId = int.Parse(stepContext.GetValueFromJObject(CardType.DentistId));
        var cardJson = await TemplateCardLoader.LoadAppoinmentDateCardAsync();
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateDateCard(cardJson),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowSchedules(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var appoinment  = stepContext.GetAppoinment();
        appoinment.AppoinmentDate = DateTime.Parse(stepContext.GetValueFromJObject(CardType.Date));
        var response = await _botService.GetAvailableHoursAsync(new AvailableTimeRangePostDto
        {
            DentistId       = appoinment.DentistId,
            DentalServiceId = appoinment.GeneralTreatmentId,
            AppointmentDate = appoinment.AppoinmentDate
        });

        if (!response.Success)
        {
            throw new Exception(response.Message);
        }

        var availableHours = response.Data as List<AvailableTimeRangeDto>;
        await stepContext.Context.SendActivityAsync($"Total de horas disponibles: {availableHours.Count}.\n\nSeleccione la hora para su cita:");
        return await stepContext.PromptAsync(
            nameof(TextPrompt),
            HeroCardFactory.CreateSchedulesCarousel(availableHours),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowAppointmentData(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var appoinment       = stepContext.GetAppoinment();
        var timeRange        = stepContext.GetValueFromString().Split("-");
        appoinment.StartHour = TimeSpan.Parse(timeRange[0]);
        appoinment.EndHour   = TimeSpan.Parse(timeRange[1]);
        var response = await _botService.CreateScheduledAppoinmentAsync(appoinment);
        if (!response.Success)
        {
            throw new Exception(response.Message);
        }

        var rangeToPay = await _botService.GetRangeToPayAsync(appoinment.GeneralTreatmentId);
        var msg = rangeToPay.PriceMin != rangeToPay.PriceMax ? 
                    $"El rango a pagar es de ${rangeToPay.PriceMin} a ${rangeToPay.PriceMax}" :
                    $"El valor a pagar es de ${rangeToPay.PriceMax}";

        await stepContext.Context.SendActivityAsync($"Cita agendada con éxito. {msg}.", cancellationToken: cancellationToken);
        await stepContext.Context.SendActivityAsync("Gracias por usar nuestro servicio.\n\n" +
                "Si desea agendar otra cita, escriba algo para empezar de nuevo el proceso.", cancellationToken: cancellationToken);
        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
    }
}
