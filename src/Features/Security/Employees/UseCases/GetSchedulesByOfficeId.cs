namespace DentallApp.Features.Security.Employees.UseCases;

public class GetSchedulesByOfficeIdResponse
{
    public class Schedule
    {
        public int WeekDayId { get; init; }
        public string WeekDayName { get; init; }
        public string MorningStartHour { get; init; }
        public string MorningEndHour { get; init; }
        public string AfternoonStartHour { get; init; }
        public string AfternoonEndHour { get; init; }
    }

    public string FullName { get; init; }
    public bool IsEmployeeDeleted { get; init; }
    public IEnumerable<Schedule> Schedules { get; init; }
}

public class GetSchedulesByOfficeIdUseCase
{
    private readonly AppDbContext _context;

    public GetSchedulesByOfficeIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetSchedulesByOfficeIdResponse>> Execute(int officeId)
    {
        var employeeSchedules = await _context.Set<Employee>()
            .Where(employee => employee.OfficeId == officeId && employee.EmployeeSchedules.Any())
            .Select(employee => new GetSchedulesByOfficeIdResponse
            {
                FullName          = employee.Person.FullName,
                IsEmployeeDeleted = employee.IsDeleted,
                Schedules = employee.EmployeeSchedules
                    .Select(employeeSchedule => new GetSchedulesByOfficeIdResponse.Schedule
                    {
                        WeekDayId          = employeeSchedule.WeekDayId,
                        WeekDayName        = employeeSchedule.WeekDay.Name,
                        MorningStartHour   = employeeSchedule.MorningStartHour.GetHourWithoutSeconds(),
                        MorningEndHour     = employeeSchedule.MorningEndHour.GetHourWithoutSeconds(),
                        AfternoonStartHour = employeeSchedule.AfternoonStartHour.GetHourWithoutSeconds(),
                        AfternoonEndHour   = employeeSchedule.AfternoonEndHour.GetHourWithoutSeconds()
                    })
                    .OrderBy(scheduleDto => scheduleDto.WeekDayId)
                    .ToList()
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return employeeSchedules;
    }
}
