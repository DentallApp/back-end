namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleRepository : SoftDeleteRepository<EmployeeSchedule>, IEmployeeScheduleRepository
{
    public EmployeeScheduleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<EmployeeScheduleGetAllDto>> GetAllEmployeeSchedulesAsync(int officeId)
        => await Context.Set<Employee>()
                        .Include(employee => employee.Person)
                        .Include(employee => employee.EmployeeSchedules)
                           .ThenInclude(employeeSchedule => employeeSchedule.WeekDay)
                        .Where(employee => employee.OfficeId == officeId && employee.EmployeeSchedules.Any())
                        .Select(employee => employee.MapToEmployeeScheduleGetAllDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId)
        => await Context.Set<EmployeeSchedule>()
                        .Include(employeeSchedule => employeeSchedule.WeekDay)
                        .Where(employeeSchedule => employeeSchedule.EmployeeId == employeeId)
                        .OrderBy(employeeSchedule => employeeSchedule.WeekDayId)
                        .Select(employeeSchedule => employeeSchedule.MapToEmployeeScheduleGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<EmployeeSchedule> GetEmployeeScheduleByIdAsync(int scheduleId)
        => await Context.Set<EmployeeSchedule>()
                        .Where(employeeSchedule => employeeSchedule.Id == scheduleId)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();

    public async Task<EmployeeScheduleByWeekDayDto> GetEmployeeScheduleByWeekDayIdAsync(int employeeId, int weekDayId)
        => await (from employeeSchedule in Context.Set<EmployeeSchedule>()
                  join employee in Context.Set<Employee>() on employeeSchedule.EmployeeId equals employee.Id
                  join office in Context.Set<Office>() on employee.OfficeId equals office.Id
                  join officeSchedule in Context.Set<OfficeSchedule>() on 
                    new
                    {
                        WeekDayId = weekDayId,
                        office.Id
                    }
                    equals
                    new
                    {
                        officeSchedule.WeekDayId,
                        Id = officeSchedule.OfficeId
                    }
                  where employee.Id == employeeId && employeeSchedule.WeekDayId == weekDayId
                  select new EmployeeScheduleByWeekDayDto
                  {
                      EmployeeScheduleId            = employeeSchedule.Id,
                      MorningStartHour              = employeeSchedule.MorningStartHour,
                      MorningEndHour                = employeeSchedule.MorningEndHour,
                      AfternoonStartHour            = employeeSchedule.AfternoonStartHour,
                      AfternoonEndHour              = employeeSchedule.AfternoonEndHour,
                      IsEmployeeScheculeDeleted     = employeeSchedule.IsDeleted,
                      OfficeId                      = employee.OfficeId,
                      IsOfficeDeleted               = office.IsDeleted,
                      IsOfficeScheduleDeleted       = officeSchedule.IsDeleted
                  })
                 .IgnoreQueryFilters()
                 .FirstOrDefaultAsync();


}
