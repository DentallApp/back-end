namespace DentallApp.Features.Appointments.UseCases;

public class GetAppointmentInformationResponse
{
    public string PatientName { get; init; }
    public string CellPhone { get; init; }
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
    public string DentalServiceName { get; init; }
}

public class SendAppointmentInformationUseCase
{
    private readonly AppDbContext _context;
    private readonly IInstantMessaging _instantMessaging;
    private readonly ITreatmentRepository _treatmentRepository;

    public SendAppointmentInformationUseCase(
        AppDbContext context, 
        IInstantMessaging instantMessaging, 
        ITreatmentRepository treatmentRepository)
    {
        _context = context;
        _instantMessaging = instantMessaging;
        _treatmentRepository = treatmentRepository;
    }

    public async Task ExecuteAsync(int appointmentId, CreateAppointmentRequest request)
    {
        // The query is executed in case the scheduling of appointments is done manually by the secretary.
        request.RangeToPay ??= await _treatmentRepository.GetRangeToPayAsync(request.GeneralTreatmentId);
        var businessName = EnvReader.Instance[AppSettings.BusinessName];
        var appointmentInfo = await GetAppointmentInformationAsync(appointmentId);
        var msg = string.Format(AppointmentInformationMessageTemplate,
            appointmentInfo.PatientName,
            businessName,
            appointmentInfo.DentistName,
            appointmentInfo.OfficeName,
            appointmentInfo.DentalServiceName,
            request.AppointmentDate.GetDateInSpanishFormat(),
            request.StartHour.GetHourWithoutSeconds(),
            request.RangeToPay?.Description);
        await _instantMessaging.SendMessageAsync(appointmentInfo.CellPhone, msg);
    }

    private Task<GetAppointmentInformationResponse> GetAppointmentInformationAsync(int appointmentId)
    {
        return _context.Set<Appointment>()
            .Where(appointment => appointment.Id == appointmentId)
            .Select(appointment => new GetAppointmentInformationResponse
            {
                PatientName       = appointment.Person.FullName,
                CellPhone         = appointment.Person.CellPhone,
                DentistName       = appointment.Employee.Person.FullName,
                DentalServiceName = appointment.GeneralTreatment.Name,
                OfficeName        = appointment.Office.Name
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
