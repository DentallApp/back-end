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
}
