namespace DentallApp.UnitTests.Features.Chatbot.Dialogs;

public static class ActivityFactory
{
    public static Activity CreateInitialActivity()
        => new()
        {
            Text = "Hi",
            From = new ChannelAccount { Id = "1", Name = "daveseva2010@hotmail.es" },
            ChannelData = JObject.Parse(@"
            {
                personId: 1,
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
