namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleRepository : IEmployeeScheduleRepository
{
    private readonly AppDbContext _context;

    public EmployeeScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeekDayDto>> GetEmployeeScheduleWithOnlyWeekDayAsync(int employeeId)
    { 
        var schedules = await _context.Set<EmployeeSchedule>()
            .Where(employeeSchedule => employeeSchedule.EmployeeId == employeeId)
            .OrderBy(employeeSchedule => employeeSchedule.WeekDayId)
            .Select(employeeSchedule => new WeekDayDto
            {
                WeekDayId   = employeeSchedule.WeekDayId,
                WeekDayName = employeeSchedule.WeekDay.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return schedules;
    }

    public async Task<EmployeeScheduleByWeekDayDto> GetEmployeeScheduleByWeekDayIdAsync(int employeeId, int weekDayId)
    { 
        var schedules = await 
            (from employeeSchedule in _context.Set<EmployeeSchedule>()
                join employee in _context.Set<Employee>() on employeeSchedule.EmployeeId equals employee.Id
                join office in _context.Set<Office>() on employee.OfficeId equals office.Id
                join officeSchedule in _context.Set<OfficeSchedule>() on
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
                    EmployeeScheduleId        = employeeSchedule.Id,
                    MorningStartHour          = employeeSchedule.MorningStartHour,
                    MorningEndHour            = employeeSchedule.MorningEndHour,
                    AfternoonStartHour        = employeeSchedule.AfternoonStartHour,
                    AfternoonEndHour          = employeeSchedule.AfternoonEndHour,
                    IsEmployeeScheculeDeleted = employeeSchedule.IsDeleted,
                    OfficeId                  = employee.OfficeId,
                    IsOfficeDeleted           = office.IsDeleted,
                    IsOfficeScheduleDeleted   = officeSchedule.IsDeleted
                })
                .IgnoreQueryFilters()
                .AsNoTracking()
                .FirstOrDefaultAsync();

        return schedules;
    }
}
