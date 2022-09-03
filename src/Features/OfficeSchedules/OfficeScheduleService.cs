namespace DentallApp.Features.OfficeSchedules;

public class OfficeScheduleService : IOfficeScheduleService
{
    private readonly IOfficeScheduleRepository _officeScheduleRepository;

    public OfficeScheduleService(IOfficeScheduleRepository officeScheduleRepository)
    {
        _officeScheduleRepository = officeScheduleRepository;
    }

    public async Task<Response> CreateOfficeScheduleAsync(ClaimsPrincipal currentEmployee, OfficeScheduleInsertDto officeScheduleInsertDto)
    {
        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeScheduleInsertDto.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        _officeScheduleRepository.Insert(officeScheduleInsertDto.MapToOfficeSchedule());
        await _officeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync()
        => await _officeScheduleRepository.GetAllOfficeSchedulesAsync();

    public async Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedulesAsync()
        => await _officeScheduleRepository.GetHomePageSchedulesAsync();

    public async Task<IEnumerable<OfficeScheduleGetDto>> GetOfficeScheduleByOfficeIdAsync(int officeId)
        => await _officeScheduleRepository.GetOfficeScheduleByOfficeIdAsync(officeId);

    public async Task<Response> UpdateOfficeScheduleAsync(int scheduleId, ClaimsPrincipal currentEmployee, OfficeScheduleUpdateDto officeScheduleUpdateDto)
    {
        var officeSchedule = await _officeScheduleRepository.GetOfficeScheduleByIdAsync(scheduleId);
        if (officeSchedule is null)
            return new Response(ResourceNotFoundMessage);

        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(officeSchedule.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        officeScheduleUpdateDto.MapToOfficeSchedule(officeSchedule);
        await _officeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
