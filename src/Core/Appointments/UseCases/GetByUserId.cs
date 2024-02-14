namespace DentallApp.Core.Appointments.UseCases;

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

public class GetAppointmentsByUserIdUseCase(DbContext context)
{
    public async Task<IEnumerable<GetAppointmentsByUserIdResponse>> ExecuteAsync(int userId)
    {
        var appointments = await 
            (from appointment in context.Set<Appointment>()
             join appointmentStatus in context.Set<AppointmentStatus>() on appointment.AppointmentStatusId equals appointmentStatus.Id
             join patientDetails in context.Set<Person>() on appointment.PersonId equals patientDetails.Id
             join dentist in context.Set<Employee>() on appointment.DentistId equals dentist.Id
             join dentistDetails in context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
             join office in context.Set<Office>() on appointment.OfficeId equals office.Id
             join generalTreatment in context.Set<GeneralTreatment>() on appointment.GeneralTreatmentId equals generalTreatment.Id
             join dependent in context.Set<Dependent>() on patientDetails.Id equals dependent.PersonId into dependents
             from dependent in dependents.DefaultIfEmpty()
             join kinship in context.Set<Kinship>() on dependent.KinshipId equals kinship.Id into kinships
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
                 KinshipName       = kinship == null ? KinshipName.Default : kinship.Name
             })
             .IgnoreQueryFilters()
             .AsNoTracking()
             .ToListAsync();

        return appointments;
    }
}
