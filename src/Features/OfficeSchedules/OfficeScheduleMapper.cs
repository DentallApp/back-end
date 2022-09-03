namespace DentallApp.Features.OfficeSchedules;

public static class OfficeScheduleMapper
{
    public static OfficeSchedule MapToOfficeSchedule(this OfficeScheduleInsertDto officeScheduleInsertDto)
        => new()
        {
            WeekDayId    = officeScheduleInsertDto.WeekDayId,
            OfficeId     = officeScheduleInsertDto.OfficeId,
            StartHour    = officeScheduleInsertDto.StartHour,
            EndHour      = officeScheduleInsertDto.EndHour
        };

    public static void MapToOfficeSchedule(this OfficeScheduleUpdateDto officeScheduleUpdateDto, OfficeSchedule officeSchedule)
    {
        officeSchedule.WeekDayId = officeScheduleUpdateDto.WeekDayId;
        officeSchedule.StartHour = officeScheduleUpdateDto.StartHour;
        officeSchedule.EndHour   = officeScheduleUpdateDto.EndHour;
        officeSchedule.IsDeleted = officeScheduleUpdateDto.IsDeleted;
    }

    [Decompile]
    public static OfficeScheduleGetDto MapToOfficeScheduleGetDto(this OfficeSchedule officeSchedule)
        => new()
        {
            ScheduleId  = officeSchedule.Id,
            WeekDayId   = officeSchedule.WeekDayId,
            WeekDayName = officeSchedule.WeekDay.Name,
            Status      = officeSchedule.GetStatusName(),
            IsDeleted   = officeSchedule.IsDeleted,
            StartHour   = officeSchedule.StartHour.GetHourWithoutSeconds(),
            EndHour     = officeSchedule.EndHour.GetHourWithoutSeconds()
        };

    [Decompile]
    public static OfficeScheduleGetAllDto MapToOfficeScheduleGetAllDto(this Office office)
        => new()
        {
            Name            = office.Name,
            IsOfficeDeleted = office.IsDeleted,
            Schedules       = office.OfficeSchedules.Select(officeSchedule => officeSchedule.MapToOfficeScheduleDto())
                                                    .OrderBy(officeScheduleDto => officeScheduleDto.WeekDayId)
                                                    .ToList()
        };

    [Decompile]
    public static OfficeScheduleShowDto MapToOfficeScheduleShowDto(this Office office)
        => new()
        {
            Name            = office.Name,
            Address         = office.Address,
            ContactNumber   = office.ContactNumber,
            Schedules       = office.OfficeSchedules.Select(officeSchedule => officeSchedule.MapToOfficeScheduleDto())
                                                    .OrderBy(officeScheduleDto => officeScheduleDto.WeekDayId)
                                                    .ToList()
        };

    [Decompile]
    public static OfficeScheduleDto MapToOfficeScheduleDto(this OfficeSchedule officeSchedule)
        => new()
        {
            WeekDayId   = officeSchedule.WeekDayId,
            WeekDayName = officeSchedule.WeekDay.Name,
            StartHour   = officeSchedule.StartHour.GetHourWithoutSeconds(),
            EndHour     = officeSchedule.EndHour.GetHourWithoutSeconds()
        };
}
