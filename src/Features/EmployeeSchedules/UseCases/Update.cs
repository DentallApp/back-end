﻿namespace DentallApp.Features.EmployeeSchedules.UseCases;

public class UpdateEmployeeScheduleRequest
{
    public int WeekDayId { get; init; }
    public bool IsDeleted { get; init; }
    public TimeSpan? MorningStartHour { get; init; }
    public TimeSpan? MorningEndHour { get; init; }
    public TimeSpan? AfternoonStartHour { get; init; }
    public TimeSpan? AfternoonEndHour { get; init; }
}

public static class UpdateEmployeeScheduleMapper
{
    public static void MapToEmployeeSchedule(this UpdateEmployeeScheduleRequest request, EmployeeSchedule schedule)
    {
        schedule.WeekDayId          = request.WeekDayId;
        schedule.IsDeleted          = request.IsDeleted;
        schedule.MorningStartHour   = request.MorningStartHour;
        schedule.MorningEndHour     = request.MorningEndHour;
        schedule.AfternoonStartHour = request.AfternoonStartHour;
        schedule.AfternoonEndHour   = request.AfternoonEndHour;
    }
}

public class UpdateEmployeeScheduleUseCase
{
    private readonly AppDbContext _context;

    public UpdateEmployeeScheduleUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id, UpdateEmployeeScheduleRequest request)
    {
        var employeeSchedule = await _context.Set<EmployeeSchedule>()
            .Where(employeeSchedule => employeeSchedule.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (employeeSchedule is null)
            return new Response(ResourceNotFoundMessage);

        request.MapToEmployeeSchedule(employeeSchedule);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
