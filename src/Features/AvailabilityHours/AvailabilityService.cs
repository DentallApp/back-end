namespace DentallApp.Features.AvailabilityHours;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAppoinmentRepository _appoinmentRepository;
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;
    private readonly IGeneralTreatmentRepository _dentalServiceRepository;

    public AvailabilityService(IAppoinmentRepository appoinmentRepository,
                               IEmployeeScheduleRepository employeeScheduleRepository,
                               IGeneralTreatmentRepository dentalServiceRepository)
    {
        _appoinmentRepository = appoinmentRepository;
        _employeeScheduleRepository = employeeScheduleRepository;
        _dentalServiceRepository = dentalServiceRepository;
    }

    /// <summary>
    /// Obtiene el tiempo libre (o punto de descanso) del empleado. 
    /// Este tiempo se descarta para el cálculo de los horarios disponibles.
    /// </summary>
    private UnavailableTimeRangeDto GetTimeOff(EmployeeScheduleByWeekDayDto employeeScheduleDto)
        => new()
        {
            StartHour = (TimeSpan)employeeScheduleDto.MorningEndHour,
            EndHour   = (TimeSpan)employeeScheduleDto.AfternoonStartHour
        };

    public async Task<Response<IEnumerable<AvailableTimeRangeDto>>> GetAvailableHoursAsync(AvailableTimeRangePostDto availableTimeRangePostDto)
    {
        int dentistId            = availableTimeRangePostDto.DentistId;
        int serviceId            = availableTimeRangePostDto.DentalServiceId;
        var appoinmentDate       = availableTimeRangePostDto.AppointmentDate;
        int weekDayId            = (int)appoinmentDate.DayOfWeek;
        var weekDayName          = WeekDaysType.WeekDays[weekDayId];
        var employeeScheduleDto  = await _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(dentistId, weekDayId);
        if (employeeScheduleDto is null || employeeScheduleDto.IsEmployeeScheculeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(string.Format(DentistNotAvailableMessage, weekDayName));

        if (employeeScheduleDto.IsOfficeScheduleDeleted || employeeScheduleDto.IsOfficeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(string.Format(OfficeClosedForSpecificDayMessage, weekDayName));

        if (employeeScheduleDto.HasNotSchedule())
            return new Response<IEnumerable<AvailableTimeRangeDto>>(NoMorningOrAfternoonHoursMessage);

        var unavailables      = await _appoinmentRepository.GetUnavailableHoursAsync(dentistId, appoinmentDate);
        var dentalServiceDto  = await  _dentalServiceRepository.GetTreatmentWithDurationAsync(serviceId);
        if (dentalServiceDto is null)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(DentalServiceNotAvailableMessage);

        TimeSpan serviceDuration = TimeSpan.FromMinutes(dentalServiceDto.Duration);
        List<AvailableTimeRangeDto> availableHours = null;

        if (employeeScheduleDto.HasFullSchedule())
        {
            unavailables.Add(GetTimeOff(employeeScheduleDto));
            availableHours = Availability.GetAvailableHours(new AvailabilityOptions
            {
                DentistStartHour = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour   = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration  = serviceDuration,
                Unavailables     = unavailables.OrderBy(timeRange => timeRange.StartHour)
                                               .ThenBy(timeRange => timeRange.EndHour)
                                               .ToList()
            });
        }
        else if(employeeScheduleDto.IsMorningSchedule())
        {
            availableHours = Availability.GetAvailableHours(new AvailabilityOptions
            {
                DentistStartHour = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour   = (TimeSpan)employeeScheduleDto.MorningEndHour,
                ServiceDuration  = serviceDuration,
                Unavailables     = unavailables
            });
        }
        else if(employeeScheduleDto.IsAfternoonSchedule())
        {
            availableHours = Availability.GetAvailableHours(new AvailabilityOptions
            {
                DentistStartHour = (TimeSpan)employeeScheduleDto.AfternoonStartHour,
                DentistEndHour   = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration  = serviceDuration,
                Unavailables     = unavailables
            });
        }

        if(availableHours is null)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(NoSchedulesAvailableMessage);

        return new Response<IEnumerable<AvailableTimeRangeDto>>
        {
            Success = true,
            Data = availableHours,
            Message = GetResourceMessage
        };
    }
}
