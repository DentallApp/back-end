namespace DentallApp.Features.ChatBot;

/// <summary>
/// Representa los tipos de tarjetas que utiliza el chatbot para la aplicación.
/// Por ejemplo: PatientCard, OfficeCard, etc.
/// </summary>
public static class CardType
{
    public const string PatientId       = "patientId";
    public const string OfficeId        = "officeId";
    public const string DentistId       = "dentistId";
    public const string DentalServiceId = "dentalServiceId";
    public const string DateId          = "date";

    public static string GetSelectedPatientId(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromJObject(PatientId);

    public static string GetSelectedOfficeId(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromJObject(OfficeId);

    public static string GetSelectedDentalServiceId(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromJObject(DentalServiceId);

    public static string GetSelectedDentistId(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromJObject(DentistId);

    public static string GetSelectedAppointmentDate(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromJObject(DateId);

    public static string GetSelectedTimeRange(this WaterfallStepContext stepContext)
        => stepContext.GetValueFromString();
}
