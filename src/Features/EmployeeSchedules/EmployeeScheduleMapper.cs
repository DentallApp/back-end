namespace DentallApp.Features.EmployeeSchedules;

public static class EmployeeScheduleMapper
{
    public static EmployeeSchedule MapToEmployeeSchedule(this EmployeeScheduleInsertDto employeeScheduleDto)
        => new()
        {
            EmployeeId           = employeeScheduleDto.EmployeeId,
            WeekDayId            = employeeScheduleDto.WeekDayId,
            MorningStartHour     = employeeScheduleDto.MorningStartHour,
            MorningEndHour       = employeeScheduleDto.MorningEndHour,
            AfternoonStartHour   = employeeScheduleDto.AfternoonStartHour,
            AfternoonEndHour     = employeeScheduleDto.AfternoonEndHour
        };

    public static void MapToEmployeeSchedule(this EmployeeScheduleUpdateDto employeeScheduleDto, EmployeeSchedule employeeSchedule)
    {
        employeeSchedule.WeekDayId          = employeeScheduleDto.WeekDayId;
        employeeSchedule.IsDeleted          = employeeScheduleDto.IsDeleted;
        employeeSchedule.MorningStartHour   = employeeScheduleDto.MorningStartHour;
        employeeSchedule.MorningEndHour     = employeeScheduleDto.MorningEndHour;
        employeeSchedule.AfternoonStartHour = employeeScheduleDto.AfternoonStartHour;
        employeeSchedule.AfternoonEndHour   = employeeScheduleDto.AfternoonEndHour;
    }

    [Decompile]
    public static EmployeeScheduleGetDto MapToEmployeeScheduleGetDto(this EmployeeSchedule employeeSchedule)
        => new()
        {
            ScheduleId          = employeeSchedule.Id,
            WeekDayId           = employeeSchedule.WeekDayId,
            WeekDayName         = employeeSchedule.WeekDay.Name,
            Status              = employeeSchedule.GetStatusName(),
            IsDeleted           = employeeSchedule.IsDeleted,
            MorningStartHour    = employeeSchedule.MorningStartHour.GetHourWithoutSeconds(),
            MorningEndHour      = employeeSchedule.MorningEndHour.GetHourWithoutSeconds(),
            AfternoonStartHour  = employeeSchedule.AfternoonStartHour.GetHourWithoutSeconds(),
            AfternoonEndHour    = employeeSchedule.AfternoonEndHour.GetHourWithoutSeconds()
        };
}
