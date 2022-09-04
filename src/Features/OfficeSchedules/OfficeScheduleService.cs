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
    { 
        var offices = await _officeScheduleRepository.GetAllOfficeSchedulesAsync() as List<OfficeScheduleGetAllDto>;
        foreach (var office in offices)
            office.Schedules = AddMissingSchedules(office.Schedules);
        return offices;
    }

    public async Task<IEnumerable<OfficeScheduleShowDto>> GetHomePageSchedulesAsync()
    { 
        var offices = await _officeScheduleRepository.GetHomePageSchedulesAsync() as List<OfficeScheduleShowDto>;
        foreach (var office in offices)
            office.Schedules = AddMissingSchedules(office.Schedules, OfficeClosedMessage);
        return offices;
    }

    /// <summary>
    /// Agrega los horarios que faltan dentro de <c>schedules</c>.
    /// </summary>
    /// <param name="schedules">Los horarios actuales.</param>
    /// <param name="notAvailable">Un valor opcional que indica sí el horario no está disponible.</param>
    /// <returns>
    /// Una colección con los horarios que faltaban y los que ya estaban.
    /// Sí no falta ningún horario, entonces se devuelve la misma colección que se pasó por parámetro.
    /// </returns>
    private List<OfficeScheduleDto> AddMissingSchedules(List<OfficeScheduleDto> schedules, string notAvailable = NotAvailableMessage)
    {
        if (schedules.Count() == WeekDaysType.MaxWeekDay)
            return schedules;

        foreach (var (weekDayId, weekDayName) in WeekDaysType.WeekDays)
        {
            if (schedules.Find(scheduleDto => scheduleDto.WeekDayId == weekDayId) is null)
            {
                var newScheduleDto = new OfficeScheduleDto
                {
                    WeekDayId   = weekDayId,
                    WeekDayName = weekDayName,
                    Schedule    = notAvailable
                };
                schedules.Add(newScheduleDto);
            }
        }
        return schedules.OrderBy(scheduleDto => scheduleDto.WeekDayId).ToList();
    }

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
