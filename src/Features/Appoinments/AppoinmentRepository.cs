using LinqToDB;
using LinqToDB.EntityFrameworkCore;

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
                 .ToListAsyncEF();

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
                        .ToListAsyncEF();

    public async Task<bool> IsNotAvailableAsync(AppoinmentInsertDto appoinmentDto)
    {
        var result = await Context.Set<Appoinment>()
                                  .Where(appoinment => 
                                        (appoinment.DentistId == appoinmentDto.DentistId) &&
                                        (appoinment.Date == appoinmentDto.AppoinmentDate) &&
                                        (appoinment.StartHour == appoinmentDto.StartHour) &&
                                        (appoinment.EndHour == appoinmentDto.EndHour) &&
                                        (appoinment.IsNotCanceled() ||
                                         appoinment.IsCancelledByEmployee ||
                                         DateTime.Now > Context.AddTime(Context.ToDateTime(appoinment.Date), appoinment.StartHour)))
                                  .Select(appoinment => appoinment.Id)
                                  .FirstOrDefaultAsyncEF();

        return result != 0;
    }

    public async Task<IEnumerable<AppoinmentGetByEmployeeDto>> GetAppoinmentsForEmployeeAsync(AppoinmentPostDateDto appoinmentPostDto)
        => await Context.Set<Appoinment>()
                        .Include(appoinment => appoinment.Person)
                        .Include(appoinment => appoinment.AppoinmentStatus)
                        .Include(appoinment => appoinment.GeneralTreatment)
                        .Include(appoinment => appoinment.Employee)
                          .ThenInclude(employee => employee.Person)
                        .Include(appoinment => appoinment.Office)
                        .OptionalWhere(appoinmentPostDto.StatusId,  appoinment => appoinment.AppoinmentStatusId == appoinmentPostDto.StatusId)
                        .OptionalWhere(appoinmentPostDto.OfficeId,  appoinment => appoinment.OfficeId == appoinmentPostDto.OfficeId)
                        .OptionalWhere(appoinmentPostDto.DentistId, appoinment => appoinment.DentistId == appoinmentPostDto.DentistId)
                        .Where(appoinment => appoinment.Date >= appoinmentPostDto.From && appoinment.Date <= appoinmentPostDto.To)
                        .OrderBy(appoinment => appoinment.Date)
                        .Select(appoinment => appoinment.MapToAppoinmentGetByEmployeeDto())
                        .IgnoreQueryFilters()
                        .ToListAsyncEF();


    /// <summary>
    /// Cancela las citas por parte del empleado.
    /// </summary>
    private async Task<int> CancelAppointmentsForEmployeeAsync(int officeId, int dentistId, IEnumerable<int> appoinmentsId)
        => appoinmentsId.Count() == 0 ? default : 
           await Context.Set<Appoinment>()
                        .OptionalWhere(officeId,  appoinment => appoinment.OfficeId == officeId)
                        .OptionalWhere(dentistId, appoinment => appoinment.DentistId == dentistId)
                        .Where(appoinment =>
                               appoinment.AppoinmentStatusId == AppoinmentStatusId.Scheduled &&
                               appoinmentsId.Contains(appoinment.Id))
                        .Set(appoinment => appoinment.AppoinmentStatusId, AppoinmentStatusId.Canceled)
                        .Set(appoinment => appoinment.IsCancelledByEmployee, true)
                        .Set(appoinment => appoinment.UpdatedAt, DateTime.Now)
                        .UpdateAsync();

    public async Task<int> CancelAppointmentsByOfficeIdAsync(int officeId, IEnumerable<int> appoinmentsId)
        => await CancelAppointmentsForEmployeeAsync(officeId, dentistId: default, appoinmentsId);

    public async Task<int> CancelAppointmentsByDentistIdAsync(int dentistId, IEnumerable<int> appoinmentsId)
        => await CancelAppointmentsForEmployeeAsync(officeId: default, dentistId, appoinmentsId);

    public async Task<AppoinmentInfoDto> GetAppoinmentInformationAsync(int id)
        => await Context.Set<Appoinment>()
                        .Include(appoinment => appoinment.Person)
                        .Include(appoinment => appoinment.Employee.Person)
                        .Include(appoinment => appoinment.Office)
                        .Include(appoinment => appoinment.GeneralTreatment)
                        .Where(appoinment => appoinment.Id == id)
                        .Select(appoinment => appoinment.MapToAppoinmentInfoDto())
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsyncEF();
}
