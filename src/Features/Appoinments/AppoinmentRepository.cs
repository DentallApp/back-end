namespace DentallApp.Features.Appoinments;

public class AppoinmentRepository : Repository<Appoinment>, IAppoinmentRepository
{
    public AppoinmentRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId)
        => await (from appoinment in Context.Set<Appoinment>()
                  join appoinmentStatus in Context.Set<AppoinmentStatus>() on appoinment.AppoinmentStatusId equals appoinmentStatus.Id
                  join patientDetails in Context.Set<Person>() on appoinment.PersonId equals patientDetails.Id
                  join dentist in Context.Set<Employee>() on appoinment.DentistId equals dentist.Id
                  join dentistDetails in Context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
                  join office in Context.Set<Office>() on appoinment.OfficeId equals office.Id
                  join generalTreatment in Context.Set<GeneralTreatment>() on appoinment.GeneralTreatmentId equals generalTreatment.Id
                  join dependent in Context.Set<Dependent>() on patientDetails.Id equals dependent.PersonId into dependents
                  from dependent in dependents.DefaultIfEmpty()
                  join kinship in Context.Set<Kinship>() on dependent.KinshipId equals kinship.Id into kinships
                  from kinship in kinships.DefaultIfEmpty()
                  where appoinment.UserId == userId
                  orderby appoinment.CreatedAt descending
                  select new AppoinmentGetByBasicUserDto
                  {
                      AppoinmentId      = appoinment.Id,
                      PatientName       = patientDetails.FullName,
                      Status            = appoinmentStatus.Name,
                      CreatedAt         = appoinment.CreatedAt.GetDateAndHourInSpanishFormat(),
                      AppointmentDate   = appoinment.Date.GetDateInSpanishFormat(),
                      StartHour         = appoinment.StartHour.GetHourWithoutSeconds(),
                      EndHour           = appoinment.EndHour.GetHourWithoutSeconds(),
                      DentistName       = dentistDetails.FullName,
                      DentalServiceName = generalTreatment.Name,
                      OfficeName        = office.Name,
                      KinshipName       = kinship == null ? KinshipsName.Default : kinship.Name
                  })
                 .IgnoreQueryFilters()
                 .ToListAsync();

    public async Task<List<UnavailableTimeRangeDto>> GetUnavailableHoursAsync(int dentistId, DateTime appoinmentDate)
        => await Context.Set<Appoinment>()
                        .Where(appoinment => 
                              (appoinment.DentistId == dentistId) &&
                              (appoinment.Date == appoinmentDate) &&
                              (appoinment.IsNotCanceled() ||
                               appoinment.IsCancelledByEmployee ||
                               DateTime.Now > Context.AddTime(Context.ToDateTime(appoinment.Date), appoinment.StartHour)))
                        .Select(appoinment => appoinment.MapToUnavailableTimeRangeDto())
                        .Distinct()
                        .OrderBy(appoinment => appoinment.StartHour)
                          .ThenBy(appoinment => appoinment.EndHour)
                        .ToListAsync();
}
