﻿namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private const string SelectDentalServiceMessage  = "Error. Escoja un servicio dental";
    private const string SelectDentistMessage        = "Error. Escoja un odontólogo";
    private const string SelectPatientMessage        = "Error. Escoja un paciente";
    private const string SelectOfficeMessage         = "Error. Escoja un consultorio";
    private const string SelectAppoinmentDateMessage = "Error. Escoja una fecha válida";
    private const string SelectScheduleMessage       = "Error. Escoja un horario";
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
        var choicesTask  = _botService.GetPatientsAsync(userProfile);
        var cardJsonTask = TemplateCardLoader.LoadPatientCardAsync();
        var choices      = await choicesTask;
        var cardJson     = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfOffices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckNextStepHasNotSentResult())
        {
            var selectedPatientId = stepContext.GetSelectedPatientId();
            if (selectedPatientId is null)
                return await stepContext.PreviousAsync(message: SelectPatientMessage, cancellationToken: cancellationToken);
            stepContext.GetAppoinment().PersonId = int.Parse(selectedPatientId);
        }
        var choicesTask  = _botService.GetOfficesAsync();
        var cardJsonTask = TemplateCardLoader.LoadOfficeCardAsync();
        var choices      = await choicesTask;
        var cardJson     = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfServices(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckNextStepHasNotSentResult())
        {
            var selectedOfficeId = stepContext.GetSelectedOfficeId();
            if (selectedOfficeId is null)
                return await stepContext.PreviousAsync(message: SelectOfficeMessage, cancellationToken: cancellationToken);
            stepContext.GetAppoinment().OfficeId = int.Parse(selectedOfficeId);
        }
        var choicesTask  = _botService.GetDentalServicesAsync(); 
        var cardJsonTask = TemplateCardLoader.LoadDentalServiceCardAsync();
        var choices      = await choicesTask;
        var cardJson     = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowNameOfDentists(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckNextStepHasNotSentResult())
        {
            var selectedDentalServiceId = stepContext.GetSelectedDentalServiceId();
            if (selectedDentalServiceId is null)
                return await stepContext.PreviousAsync(message: SelectDentalServiceMessage, cancellationToken: cancellationToken);
            stepContext.GetAppoinment().GeneralTreatmentId = int.Parse(selectedDentalServiceId);
        }
        int officeId     = stepContext.GetAppoinment().OfficeId;
        var choicesTask  = _botService.GetDentistsByOfficeIdAsync(officeId);
        var cardJsonTask = TemplateCardLoader.LoadDentistCardAsync();
        var choices      = await choicesTask;
        var cardJson     = await cardJsonTask;
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateSingleChoiceCard(cardJson, choices),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowAppoinmentDate(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckNextStepHasNotSentResult())
        {
            var selectedDentistId = stepContext.GetSelectedDentistId();
            if (selectedDentistId is null)
                return await stepContext.PreviousAsync(message: SelectDentistMessage, cancellationToken: cancellationToken);
            stepContext.GetAppoinment().DentistId = int.Parse(selectedDentistId);
        }
        var dentistScheduleTask = _botService.GetDentistScheduleAsync(stepContext.GetAppoinment().DentistId);
        var cardJsonTask        = TemplateCardLoader.LoadAppoinmentDateCardAsync();
        var dentistSchedule     = await dentistScheduleTask;
        var cardJson            = await cardJsonTask;
        await stepContext.Context.SendActivityAsync($"El odontólogo atiende los {dentistSchedule}");
        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateDateCard(cardJson),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowSchedules(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckNextStepHasNotSentResult())
        {
            var selectedAppoinmentDate = stepContext.GetSelectedAppoinmentDate();
            if (selectedAppoinmentDate is null)
                return await stepContext.PreviousAsync(message: SelectAppoinmentDateMessage, cancellationToken: cancellationToken);
            stepContext.GetAppoinment().AppoinmentDate = DateTime.Parse(selectedAppoinmentDate);
        }
        var appoinment  = stepContext.GetAppoinment();
        var response = await _botService.GetAvailableHoursAsync(new AvailableTimeRangePostDto
        {
            DentistId       = appoinment.DentistId,
            DentalServiceId = appoinment.GeneralTreatmentId,
            AppointmentDate = appoinment.AppoinmentDate
        });

        if (!response.Success)
            return await stepContext.PreviousAsync(message: response.Message, cancellationToken: cancellationToken);

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
        var appoinment = stepContext.GetAppoinment();
        try
        {
            var selectedTimeRange = stepContext.GetSelectedTimeRange();
            var timeRange         = selectedTimeRange.Split("-");
            appoinment.StartHour  = TimeSpan.Parse(timeRange[0]);
            appoinment.EndHour    = TimeSpan.Parse(timeRange[1]);
        }
        catch(FormatException)
        {
            return await stepContext.PreviousAsync(message: SelectScheduleMessage, cancellationToken: cancellationToken);
        }
        var response = await _botService.CreateScheduledAppoinmentAsync(appoinment);
        if (!response.Success)
            return await stepContext.PreviousAsync(message: response.Message, cancellationToken: cancellationToken);

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
