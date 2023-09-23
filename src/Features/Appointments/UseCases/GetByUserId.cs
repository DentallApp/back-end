namespace DentallApp.Features.Appointments.UseCases;

public class GetAppointmentsByUserIdResponse
{
    public int AppointmentId { get; init; }
    public string PatientName { get; init; }
    public string CreatedAt { get; init; }
    public string AppointmentDate { get; init; }
    public string StartHour { get; init; }
    public string EndHour { get; init; }
    public string DentalServiceName { get; init; }
    /// <summary>
    /// Obtiene o establece el nombre del parentesco.
    /// Por ejemplo: Hijo/a, Esposo/a, y Otros.
    /// </summary>
    public string KinshipName { get; init; }
    public string Status { get; init; }
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
}

public class GetAppointmentsByUserIdUseCase
{
    private readonly AppDbContext _context;

    public GetAppointmentsByUserIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetAppointmentsByUserIdResponse>> ExecuteAsync(int userId)
    {
        var appointments = await 
            (from appointment in _context.Set<Appointment>()
             join appointmentStatus in _context.Set<AppointmentStatus>() on appointment.AppointmentStatusId equals appointmentStatus.Id
             join patientDetails in _context.Set<Person>() on appointment.PersonId equals patientDetails.Id
             join dentist in _context.Set<Employee>() on appointment.DentistId equals dentist.Id
             join dentistDetails in _context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
             join office in _context.Set<Office>() on appointment.OfficeId equals office.Id
             join generalTreatment in _context.Set<GeneralTreatment>() on appointment.GeneralTreatmentId equals generalTreatment.Id
             join dependent in _context.Set<Dependent>() on patientDetails.Id equals dependent.PersonId into dependents
             from dependent in dependents.DefaultIfEmpty()
             join kinship in _context.Set<Kinship>() on dependent.KinshipId equals kinship.Id into kinships
             from kinship in kinships.DefaultIfEmpty()
             where appointment.UserId == userId
             orderby appointment.CreatedAt descending
             select new GetAppointmentsByUserIdResponse
             {
                 AppointmentId     = appointment.Id,
                 PatientName       = patientDetails.FullName,
                 Status            = appointmentStatus.Name,
                 CreatedAt         = appointment.CreatedAt.GetDateAndHourInSpanishFormat(),
                 AppointmentDate   = appointment.Date.GetDateInSpanishFormat(),
                 StartHour         = appointment.StartHour.GetHourWithoutSeconds(),
                 EndHour           = appointment.EndHour.GetHourWithoutSeconds(),
                 DentistName       = dentistDetails.FullName,
                 DentalServiceName = generalTreatment.Name,
                 OfficeName        = office.Name,
                 KinshipName       = kinship == null ? KinshipsName.Default : kinship.Name
             })
             .IgnoreQueryFilters()
             .AsNoTracking()
             .ToListAsync();

        return appointments;
    }
}
