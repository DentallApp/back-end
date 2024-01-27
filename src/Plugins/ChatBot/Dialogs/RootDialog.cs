namespace DentallApp.Features.ChatBot.Dialogs;

public partial class RootDialog : ComponentDialog
{
    private readonly IAppointmentBotService _botService;
    private readonly IDateTimeService _dateTimeService;

    public RootDialog(IAppointmentBotService botService, IDateTimeService dateTimeService) : base(nameof(RootDialog))
    {
        _botService = botService;
        _dateTimeService = dateTimeService;

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
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: Messages.SelectPatient);

        await stepContext.SendTypingActivityAsync();
        var userProfile = stepContext.CreateUserProfileInstance();
        stepContext.CreateAppointmentInstance().UserId = userProfile.UserId;
        var choicesTask  = _botService.GetPatientsAsync(userProfile);
        var cardJsonTask = AdaptiveCardsLoader.LoadPatientCardAsync();
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
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: Messages.SelectOffice);

        var selectedPatientId = stepContext.GetSelectedPatientId();
        if (selectedPatientId is null)
            return await stepContext.PreviousAsync(message: Messages.SelectPatient, cancellationToken: cancellationToken);
        stepContext.GetAppointment().PersonId = int.Parse(selectedPatientId);
        await stepContext.SendTypingActivityAsync();
        var choicesTask  = _botService.GetOfficesAsync();
        var cardJsonTask = AdaptiveCardsLoader.LoadOfficeCardAsync();
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
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: Messages.SelectDentalService);

        var selectedOfficeId = stepContext.GetSelectedOfficeId();
        if (selectedOfficeId is null)
            return await stepContext.PreviousAsync(message: Messages.SelectOffice, cancellationToken: cancellationToken);
        stepContext.GetAppointment().OfficeId = int.Parse(selectedOfficeId);
        await stepContext.SendTypingActivityAsync();
        var choicesTask  = _botService.GetDentalServicesAsync();
        var cardJsonTask = AdaptiveCardsLoader.LoadDentalServiceCardAsync();
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
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: Messages.SelectDentist);

        var selectedDentalServiceId = stepContext.GetSelectedDentalServiceId();
        if (selectedDentalServiceId is null)
            return await stepContext.PreviousAsync(message: Messages.SelectDentalService, cancellationToken: cancellationToken);
        stepContext.GetAppointment().GeneralTreatmentId = int.Parse(selectedDentalServiceId);
        await stepContext.SendTypingActivityAsync();
        int officeId     = stepContext.GetAppointment().OfficeId;
        int specialtyId  = stepContext.GetAppointment().GeneralTreatmentId;
        var choicesTask  = _botService.GetDentistsAsync(officeId, specialtyId);
        var cardJsonTask = AdaptiveCardsLoader.LoadDentistCardAsync();
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
            return await stepContext.PromptAsync(nameof(AdaptiveCardPrompt), retryMessage: Messages.SelectAppointmentDate);

        var selectedDentistId = stepContext.GetSelectedDentistId();
        if (selectedDentistId is null)
            return await stepContext.PreviousAsync(message: Messages.SelectDentist, cancellationToken: cancellationToken);
        stepContext.GetAppointment().DentistId = int.Parse(selectedDentistId);
        await stepContext.SendTypingActivityAsync();
        var dentistScheduleTask = _botService.GetDentistScheduleAsync(stepContext.GetAppointment().DentistId);
        var cardJsonTask        = AdaptiveCardsLoader.LoadAppointmentDateCardAsync();
        var dentistSchedule     = await dentistScheduleTask;
        var cardJson            = await cardJsonTask;
        await stepContext
            .Context
            .SendActivityAsync(
                string.Format(Messages.ShowScheduleToUser, dentistSchedule),
                cancellationToken: cancellationToken);

        return await stepContext.PromptAsync(
            nameof(AdaptiveCardPrompt),
            AdaptiveCardFactory.CreateDateCard(cardJson, _dateTimeService),
            cancellationToken
        );
    }

    private async Task<DialogTurnResult> ShowSchedules(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        if (stepContext.CheckIfResultNextStepIsNone())
            return await stepContext.PromptAsync(nameof(TextPrompt), retryMessage: Messages.SelectSchedule);

        var selectedAppointmentDate = stepContext.GetSelectedAppointmentDate();
        if (selectedAppointmentDate is null)
            return await stepContext.PreviousAsync(message: Messages.SelectAppointmentDate, cancellationToken: cancellationToken);
        stepContext.GetAppointment().AppointmentDate = DateTime.Parse(selectedAppointmentDate);
        await stepContext.SendTypingActivityAsync();
        var appointment = stepContext.GetAppointment();
        var result = await _botService.GetAvailableHoursAsync(new AvailableTimeRangeRequest
        {
            OfficeId        = appointment.OfficeId,
            DentistId       = appointment.DentistId,
            DentalServiceId = appointment.GeneralTreatmentId,
            AppointmentDate = appointment.AppointmentDate
        });

        if (result.IsFailed)
            return await stepContext.PreviousAsync(message: result.Message, cancellationToken: cancellationToken);

        var availableHours = result.Data as List<AvailableTimeRangeResponse>;
        await stepContext
            .Context
            .SendActivityAsync(
                string.Format(Messages.TotalHoursAvailable, availableHours.Count),
                cancellationToken: cancellationToken);

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
            var selectedTimeRange  = stepContext.GetSelectedTimeRange();
            var timeRange          = selectedTimeRange.Split("-");
            appointment.StartHour  = TimeSpan.Parse(timeRange[0]);
            appointment.EndHour    = TimeSpan.Parse(timeRange[1]);
        }
        catch(FormatException)
        {
            return await stepContext.PreviousAsync(message: Messages.SelectSchedule, cancellationToken: cancellationToken);
        }
        await stepContext.SendTypingActivityAsync();
        appointment.RangeToPay = await _botService.GetRangeToPayAsync(appointment.GeneralTreatmentId);
        var result = await _botService.CreateScheduledAppointmentAsync(appointment);
        if (result.IsFailed)
            return await stepContext.PreviousAsync(message: result.Message, cancellationToken: cancellationToken);

        await stepContext
            .Context
            .SendActivityAsync(
                string.Format(Messages.SuccessfullyScheduledAppointment, appointment.RangeToPay?.ToString()), 
                cancellationToken: cancellationToken);

        await stepContext.Context.SendActivityAsync(Messages.ThanksForUsingService, cancellationToken: cancellationToken);
        return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
    }
}
