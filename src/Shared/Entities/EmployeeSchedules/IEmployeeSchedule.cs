﻿namespace DentallApp.Shared.Entities.EmployeeSchedules;

public interface IEmployeeSchedule
{
    TimeSpan? MorningStartHour { get; set; }
    TimeSpan? MorningEndHour { get; set; }
    TimeSpan? AfternoonStartHour { get; set; }
    TimeSpan? AfternoonEndHour { get; set; }
}
