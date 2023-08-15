namespace DentallApp.Features.Security.Employees.UseCases;

public class GetEmployeesByOfficeIdResponse
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

public class GetEmployeesByOfficeIdUseCase
{
    private readonly AppDbContext _context;

    public GetEmployeesByOfficeIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetEmployeesByOfficeIdResponse>> Execute(int? officeId)
    {
        var employeeSchedules = await _context.Set<Employee>()
            .OptionalWhere(officeId, employee => employee.OfficeId == officeId)
            .Where(employee => employee.EmployeeSchedules.Any())
            .Select(employee => new GetEmployeesByOfficeIdResponse
            {
                FullName          = employee.Person.FullName,
                IsEmployeeDeleted = employee.IsDeleted,
                Schedules = employee.EmployeeSchedules
                    .Select(employeeSchedule => new GetEmployeesByOfficeIdResponse.Schedule
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
