namespace DentallApp.IntegrationTests.Features.Chatbot.Dialogs;

public static class BotServiceMockFactory
{
    public const string Id                = "2000";
    public const string PatientName       = "Dave Roman";
    public const string OfficeName        = "Mapasingue";
    public const string DentistName       = "Alice Roy";
    public const string DentalServiceName = "Ortodoncia";
    public const string Schedule          = "Lunes a Viernes";
    public const string StartHour         = "07:00";
    public const string EndHour           = "08:00";
    public const int PriceMin             = 5;
    public const int PriceMax             = 10;

    public static IAppointmentBotService CreateBotServiceMock()
    {
        var botService = Mock.Create<IAppointmentBotService>();
        Mock.Arrange(() => botService.GetPatientsAsync(Arg.IsAny<UserProfile>()))
            .ReturnsAsync((UserProfile user) => CreateChoiceLists(user.FullName, user.PersonId.ToString()));

        Mock.Arrange(() => botService.GetOfficesAsync())
            .ReturnsAsync(CreateChoiceLists(OfficeName, Id));

        Mock.Arrange(() => botService.GetDentalServicesAsync())
            .ReturnsAsync(CreateChoiceLists(DentalServiceName, Id));

        Mock.Arrange(() => botService.GetDentistsAsync(Arg.AnyInt, Arg.AnyInt))
            .ReturnsAsync(CreateChoiceLists(DentistName, Id));

        Mock.Arrange(() => botService.GetDentistScheduleAsync(Arg.AnyInt))
            .ReturnsAsync(Schedule);

        Mock.Arrange(() => botService.GetAvailableHoursAsync(Arg.IsAny<AvailableTimeRangePostDto>()))
            .ReturnsAsync(new Response<IEnumerable<AvailableTimeRangeDto>>
            {
                Success = true,
                Message = GetResourceMessage,
                Data    = new List<AvailableTimeRangeDto>
                {
                    new() { StartHour = StartHour, EndHour = EndHour }
                }
            });

        Mock.Arrange(() => botService.GetRangeToPayAsync(Arg.AnyInt))
            .ReturnsAsync(new SpecificTreatmentRangeToPayDto
            {
                PriceMin = PriceMin,
                PriceMax = PriceMax
            });

        Mock.Arrange(() => botService.CreateScheduledAppointmentAsync(Arg.IsAny<AppointmentInsertDto>()))
            .ReturnsAsync(new Response<InsertedIdDto>
            {
                Success = true,
                Data    = new InsertedIdDto { Id = int.Parse(Id) },
                Message = CreateResourceMessage
            });
        return botService;
    }

    private static List<AdaptiveChoice> CreateChoiceLists(string title, string value)
        => new() { new() { Title = title, Value = value } };
}
