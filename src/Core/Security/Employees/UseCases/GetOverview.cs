namespace DentallApp.Core.Security.Employees.UseCases;

public class GetEmployeeOverviewResponse
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

public class GetEmployeeOverviewUseCase(DbContext context, ICurrentEmployee currentEmployee)
{
    public async Task<IEnumerable<GetEmployeeOverviewResponse>> ExecuteAsync()
    {
        var employeeSchedules = await context.Set<Employee>()
            .Where(employee => employee.OfficeId == currentEmployee.OfficeId)
            .Where(employee => employee.EmployeeSchedules.Any())
            .Select(employee => new GetEmployeeOverviewResponse
            {
                FullName          = employee.Person.FullName,
                IsEmployeeDeleted = employee.IsDeleted,
                Schedules = employee.EmployeeSchedules
                    .Select(employeeSchedule => new GetEmployeeOverviewResponse.Schedule
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
