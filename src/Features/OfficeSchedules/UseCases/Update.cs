﻿namespace DentallApp.Features.OfficeSchedules.UseCases;

public class UpdateOfficeScheduleRequest
{
    public int WeekDayId { get; init; }
    public TimeSpan StartHour { get; init; }
    public TimeSpan EndHour { get; init; }
    public bool IsDeleted { get; init; }
}

public static class UpdateOfficeScheduleMapper
{
    public static void MapToOfficeSchedule(this UpdateOfficeScheduleRequest request, OfficeSchedule schedule)
    {
        schedule.WeekDayId = request.WeekDayId;
        schedule.StartHour = request.StartHour;
        schedule.EndHour   = request.EndHour;
        schedule.IsDeleted = request.IsDeleted;
    }
}

public class UpdateOfficeScheduleUseCase
{
    private readonly AppDbContext _context;

    public UpdateOfficeScheduleUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id, ClaimsPrincipal currentEmployee, UpdateOfficeScheduleRequest request)
    {
        var officeSchedule = await _context.Set<OfficeSchedule>()
            .Where(officeSchedule => officeSchedule.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        if (officeSchedule is null)
            return new Response(ResourceNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeSchedule.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        request.MapToOfficeSchedule(officeSchedule);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}