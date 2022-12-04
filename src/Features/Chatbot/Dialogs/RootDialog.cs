namespace DentallApp.Features.Chatbot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private const string SelectDentalServiceMessage   = "Error. Escoja un servicio dental";
    private const string SelectDentistMessage         = "Error. Escoja un odontólogo";
    private const string SelectPatientMessage         = "Error. Escoja un paciente";
    private const string SelectOfficeMessage          = "Error. Escoja un consultorio";
    private const string SelectAppointmentDateMessage = "Error. Escoja una fecha válida";
    private const string SelectScheduleMessage        = "Error. Escoja un horario";
    private readonly IAppointmentBotService _botService;

    public RootDialog(IAppointmentBotService botService) : base(nameof(RootDialog))
    {
        _botService = botService;

        var waterfallSteps = new WaterfallStep[]
        {
            ShowNameOfPatients,
            ShowNameOfOffices,
            ShowNameOfServices,
            ShowNameOfDentists,
            ShowAppointmentDate,
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
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: SelectPatientMessage);

        await stepContext.SendTypingActivityAsync();
        var userProfile = stepContext.CreateUserProfileInstance();
        stepContext.CreateAppointmentInstance().UserId = userProfile.Id;
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
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: SelectOfficeMessage);

        var selectedPatientId = stepContext.GetSelectedPatientId();
        if (selectedPatientId is null)
            return await stepContext.PreviousAsync(message: SelectPatientMessage, cancellationToken: cancellationToken);
        stepContext.GetAppointment().PersonId = int.Parse(selectedPatientId);
        await stepContext.SendTypingActivityAsync();
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
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: SelectDentalServiceMessage);

        var selectedOfficeId = stepContext.GetSelectedOfficeId();
        if (selectedOfficeId is null)
            return await stepContext.PreviousAsync(message: SelectOfficeMessage, cancellationToken: cancellationToken);
        stepContext.GetAppointment().OfficeId = int.Parse(selectedOfficeId);
        await stepContext.SendTypingActivityAsync();
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
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: SelectDentistMessage);

        var selectedDentalServiceId = stepContext.GetSelectedDentalServiceId();
        if (selectedDentalServiceId is null)
            return await stepContext.PreviousAsync(message: SelectDentalServiceMessage, cancellationToken: cancellationToken);
        stepContext.GetAppointment().GeneralTreatmentId = int.Parse(selectedDentalServiceId);
        await stepContext.SendTypingActivityAsync();
        int officeId     = stepContext.GetAppointment().OfficeId;
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

    private async Task<DialogTurnResult> ShowAppointmentDate(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: SelectAppointmentDateMessage);

        var selectedDentistId = stepContext.GetSelectedDentistId();
        if (selectedDentistId is null)
            return await stepContext.PreviousAsync(message: SelectDentistMessage, cancellationToken: cancellationToken);
        stepContext.GetAppointment().DentistId = int.Parse(selectedDentistId);
        await stepContext.SendTypingActivityAsync();
        var dentistScheduleTask = _botService.GetDentistScheduleAsync(stepContext.GetAppointment().DentistId);
        var cardJsonTask        = TemplateCardLoader.LoadAppointmentDateCardAsync();
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
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(TextPrompt), retryMessage: SelectScheduleMessage);

        var selectedAppointmentDate = stepContext.GetSelectedAppointmentDate();
        if (selectedAppointmentDate is null)
            return await stepContext.PreviousAsync(message: SelectAppointmentDateMessage, cancellationToken: cancellationToken);
        stepContext.GetAppointment().AppointmentDate = DateTime.Parse(selectedAppointmentDate);
        await stepContext.SendTypingActivityAsync();
        var appointment = stepContext.GetAppointment();
        var response   = await _botService.GetAvailableHoursAsync(new AvailableTimeRangePostDto
        {
            DentistId       = appointment.DentistId,
            DentalServiceId = appointment.GeneralTreatmentId,
            AppointmentDate = appointment.AppointmentDate
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
        var appointment = stepContext.GetAppointment();
        try
        {
            var selectedTimeRange = stepContext.GetSelectedTimeRange();
            var timeRange         = selectedTimeRange.Split("-");
            appointment.StartHour  = TimeSpan.Parse(timeRange[0]);
            appointment.EndHour    = TimeSpan.Parse(timeRange[1]);
        }
        catch(FormatException)
        {
            return await stepContext.PreviousAsync(message: SelectScheduleMessage, cancellationToken: cancellationToken);
        }
        await stepContext.SendTypingActivityAsync();
        appointment.RangeToPay = await _botService.GetRangeToPayAsync(appointment.GeneralTreatmentId);
        var response = await _botService.CreateScheduledAppointmentAsync(appointment);
        if (!response.Success)
            return await stepContext.PreviousAsync(message: response.Message, cancellationToken: cancellationToken);
        await stepContext.Context.SendActivityAsync($"Cita agendada con éxito. {appointment.RangeToPay}.", cancellationToken: cancellationToken);
        await stepContext.Context.SendActivityAsync("Gracias por usar nuestro servicio.\n\n" +
                "Si desea agendar otra cita, escriba algo para empezar de nuevo el proceso.", cancellationToken: cancellationToken);
        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
    }
}
