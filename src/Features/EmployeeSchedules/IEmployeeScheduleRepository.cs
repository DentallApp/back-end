﻿namespace DentallApp.Features.EmployeeSchedules;

public interface IEmployeeScheduleRepository : ISoftDeleteRepository<EmployeeSchedule>
{
    Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId);
}