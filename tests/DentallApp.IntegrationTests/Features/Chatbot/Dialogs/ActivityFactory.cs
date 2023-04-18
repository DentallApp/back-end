namespace DentallApp.IntegrationTests.Features.Chatbot.Dialogs;

public static class ActivityFactory
{
    private const string UserId   = "1000";
    private const string PersonId = "2000";

    public static Activity CreateInitialActivity()
        => new()
        {
            Text        = "Hi",
            From        = new ChannelAccount { Id = $"dl_{UserId}-{PersonId}", Name = "daveseva2010@hotmail.es" },
            ChannelData = JObject.Parse(@"
            {
                fullName: 'Dave Roman'
            }")
        };

    private static Activity CreateActivityWithJson(string json)
        => new() { Value = JObject.Parse(json) };

    public static Activity CreateActivityWithSelectedPatientId()
        => CreateActivityWithJson("{ patientId: 1 }");

    public static Activity CreateActivityWithSelectedOfficeId()
        => CreateActivityWithJson("{ officeId: 1 }");

    public static Activity CreateActivityWithSelectedDentalServiceId()
        => CreateActivityWithJson("{ dentalServiceId: 1 }");

    public static Activity CreateActivityWithSelectedDentistId()
        => CreateActivityWithJson("{ dentistId: 1 }");

    public static Activity CreateActivityWithSelectedDate()
        => CreateActivityWithJson("{ date: '2023-01-06' }");

    public static Activity CreateActivityWithSelectedSchedule()
        => new() { Value = "07:00 - 08:00" };
}
