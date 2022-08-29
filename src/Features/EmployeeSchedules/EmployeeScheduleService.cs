namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleService : IEmployeeScheduleService
{
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;

    public EmployeeScheduleService(IEmployeeScheduleRepository employeeScheduleRepository)
    {
        _employeeScheduleRepository = employeeScheduleRepository;
    }

    public async Task<Response> CreateEmployeeScheduleAsync(EmployeeScheduleInsertDto employeeScheduleDto)
    {
        var employeeSchedule = employeeScheduleDto.MapToEmployeeSchedule();
        _employeeScheduleRepository.Insert(employeeSchedule);
        await _employeeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<IEnumerable<EmployeeScheduleGetDto>> GetEmployeeScheduleByEmployeeIdAsync(int employeeId)
        => await _employeeScheduleRepository.GetEmployeeScheduleByEmployeeIdAsync(employeeId);

    public async Task<Response> UpdateEmployeeScheduleAsync(int scheduleId, EmployeeScheduleUpdateDto employeeScheduleDto)
    {
        var employeeSchedule = await _employeeScheduleRepository.GetEmployeeScheduleByIdAsync(scheduleId);
        if (employeeSchedule is null)
            return new Response(ResourceNotFoundMessage);

        employeeScheduleDto.MapToEmployeeSchedule(employeeSchedule);
        await _employeeScheduleRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
