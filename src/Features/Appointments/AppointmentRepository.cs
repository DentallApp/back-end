namespace DentallApp.Features.Appointments;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppointmentRepository(IDateTimeProvider dateTimeProvider, AppDbContext context) : base(context) 
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<IEnumerable<AppointmentGetByBasicUserDto>> GetAppointmentsByUserIdAsync(int userId)
        => await (from appointment in Context.Set<Appointment>()
                  join appointmentStatus in Context.Set<AppointmentStatus>() on appointment.AppointmentStatusId equals appointmentStatus.Id
                  join patientDetails in Context.Set<Person>() on appointment.PersonId equals patientDetails.Id
                  join dentist in Context.Set<Employee>() on appointment.DentistId equals dentist.Id
                  join dentistDetails in Context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
                  join office in Context.Set<Office>() on appointment.OfficeId equals office.Id
                  join generalTreatment in Context.Set<GeneralTreatment>() on appointment.GeneralTreatmentId equals generalTreatment.Id
                  join dependent in Context.Set<Dependent>() on patientDetails.Id equals dependent.PersonId into dependents
                  from dependent in dependents.DefaultIfEmpty()
                  join kinship in Context.Set<Kinship>() on dependent.KinshipId equals kinship.Id into kinships
                  from kinship in kinships.DefaultIfEmpty()
                  where appointment.UserId == userId
                  orderby appointment.CreatedAt descending
                  select new AppointmentGetByBasicUserDto
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
                 .ToListAsync();

    public async Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appointmentDate)
        => await Context.Set<Appointment>()
                        .Where(appointment => 
                              (appointment.DentistId == dentistId) &&
                              (appointment.Date == appointmentDate) &&
                              (appointment.IsNotCanceled() ||
                               appointment.IsCancelledByEmployee ||
                               // Checks if the canceled appointment is not available.
                               // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                               _dateTimeProvider.Now > Context.AddTime(Context.ToDateTime(appointment.Date), appointment.StartHour)))
                        .Select(appointment => appointment.MapToUnavailableTimeRangeDto())
                        .Distinct()
                        .OrderBy(appointment => appointment.StartHour)
                          .ThenBy(appointment => appointment.EndHour)
                        .ToListAsync();

    public async Task<bool> IsNotAvailableAsync(AppointmentInsertDto appointmentDto)
    {
        var result = await Context.Set<Appointment>()
                                  .Where(appointment => 
                                        (appointment.DentistId == appointmentDto.DentistId) &&
                                        (appointment.Date == appointmentDto.AppointmentDate) &&
                                        (appointment.StartHour == appointmentDto.StartHour) &&
                                        (appointment.EndHour == appointmentDto.EndHour) &&
                                        (appointment.IsNotCanceled() ||
                                         appointment.IsCancelledByEmployee ||
                                         // Checks if the canceled appointment is not available.
                                         // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                                         _dateTimeProvider.Now > Context.AddTime(Context.ToDateTime(appointment.Date), appointment.StartHour)))
                                  .Select(appointment => appointment.Id)
                                  .FirstOrDefaultAsync();

        return result != 0;
    }

    public async Task<IEnumerable<AppointmentGetByEmployeeDto>> GetAppointmentsForEmployeeAsync(AppointmentPostDateDto appointmentPostDto)
        => await Context.Set<Appointment>()
                        .Include(appointment => appointment.Person)
                        .Include(appointment => appointment.AppointmentStatus)
                        .Include(appointment => appointment.GeneralTreatment)
                        .Include(appointment => appointment.Employee)
                          .ThenInclude(employee => employee.Person)
                        .Include(appointment => appointment.Office)
                        .OptionalWhere(appointmentPostDto.StatusId,  appointment => appointment.AppointmentStatusId == appointmentPostDto.StatusId)
                        .OptionalWhere(appointmentPostDto.OfficeId,  appointment => appointment.OfficeId == appointmentPostDto.OfficeId)
                        .OptionalWhere(appointmentPostDto.DentistId, appointment => appointment.DentistId == appointmentPostDto.DentistId)
                        .Where(appointment => appointment.Date >= appointmentPostDto.From && appointment.Date <= appointmentPostDto.To)
                        .OrderBy(appointment => appointment.Date)
                        .Select(appointment => appointment.MapToAppointmentGetByEmployeeDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<AppointmentInfoDto> GetAppointmentInformationAsync(int id)
        => await Context.Set<Appointment>()
                        .Include(appointment => appointment.Person)
                        .Include(appointment => appointment.Employee.Person)
                        .Include(appointment => appointment.Office)
                        .Include(appointment => appointment.GeneralTreatment)
                        .Where(appointment => appointment.Id == id)
                        .Select(appointment => appointment.MapToAppointmentInfoDto())
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();
}
