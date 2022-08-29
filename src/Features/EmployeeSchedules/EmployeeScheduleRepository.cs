namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleRepository : SoftDeleteRepository<EmployeeSchedule>, IEmployeeScheduleRepository
{
    public EmployeeScheduleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeSchedulesAsync()
        => await Context.Set<EmployeeSchedule>()
                        .Include(employeeSchedule => employeeSchedule.WeekDay)
                        .Select(employeeSchedule => employeeSchedule.MapToEmployeeScheduleGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();
}
