namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleRepository : SoftDeleteRepository<EmployeeSchedule>, IEmployeeScheduleRepository
{
    public EmployeeScheduleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId)
        => await Context.Set<EmployeeSchedule>()
                        .Include(employeeSchedule => employeeSchedule.WeekDay)
                        .Where(employeeSchedule => employeeSchedule.EmployeeId == employeeId)
                        .Select(employeeSchedule => employeeSchedule.MapToEmployeeScheduleGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();
}
