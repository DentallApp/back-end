namespace DentallApp.Features.EmployeeSchedules;

public class EmployeeScheduleService
{
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;

    public EmployeeScheduleService(IEmployeeScheduleRepository employeeScheduleRepository)
    {
        _employeeScheduleRepository = employeeScheduleRepository;
    }

    public async Task<Response<InsertedIdDto>> CreateEmployeeScheduleAsync(EmployeeScheduleInsertDto employeeScheduleDto)
    {
        var employeeSchedule = employeeScheduleDto.MapToEmployeeSchedule();
        _employeeScheduleRepository.Insert(employeeSchedule);
        await _employeeScheduleRepository.SaveAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = employeeSchedule.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }

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
