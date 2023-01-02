namespace DentallApp.Features.AvailabilityHours;

public class AvailabilityService : IAvailabilityService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;
    private readonly IGeneralTreatmentRepository _dentalServiceRepository;
    private readonly IHolidayOfficeRepository _holidayOfficeRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AvailabilityService(IAppointmentRepository appointmentRepository,
                               IEmployeeScheduleRepository employeeScheduleRepository,
                               IGeneralTreatmentRepository dentalServiceRepository,
                               IHolidayOfficeRepository holidayOfficeRepository,
                               IDateTimeProvider dateTimeProvider)
    {
        _appointmentRepository = appointmentRepository;
        _employeeScheduleRepository = employeeScheduleRepository;
        _dentalServiceRepository = dentalServiceRepository;
        _holidayOfficeRepository = holidayOfficeRepository;
        _dateTimeProvider = dateTimeProvider;
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
        int officeId             = availableTimeRangePostDto.OfficeId;
        int dentistId            = availableTimeRangePostDto.DentistId;
        int serviceId            = availableTimeRangePostDto.DentalServiceId;
        var appointmentDate      = availableTimeRangePostDto.AppointmentDate;
        int weekDayId            = (int)appointmentDate.DayOfWeek;
        var weekDayName          = WeekDaysType.WeekDays[weekDayId];
        if (await _holidayOfficeRepository.IsPublicHolidayAsync(officeId, appointmentDate.Day, appointmentDate.Month))
            return new Response<IEnumerable<AvailableTimeRangeDto>>(AppointmentDateIsPublicHolidayMessage);

        var employeeScheduleDto  = await _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(dentistId, weekDayId);
        if (employeeScheduleDto is null || employeeScheduleDto.IsEmployeeScheculeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(string.Format(DentistNotAvailableMessage, weekDayName));

        if (employeeScheduleDto.IsOfficeScheduleDeleted || employeeScheduleDto.IsOfficeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeDto>>(string.Format(OfficeClosedForSpecificDayMessage, weekDayName));

        if (employeeScheduleDto.HasNotSchedule())
            return new Response<IEnumerable<AvailableTimeRangeDto>>(NoMorningOrAfternoonHoursMessage);

        var unavailables      = await _appointmentRepository.GetUnavailableHoursAsync(dentistId, appointmentDate);
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
                DentistStartHour   = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppoinmentDate     = appointmentDate,
                CurrentTimeAndDate = _dateTimeProvider.Now,
                Unavailables       = unavailables.OrderBy(timeRange => timeRange.StartHour)
                                                 .ThenBy(timeRange => timeRange.EndHour)
                                                 .ToList()
            });
        }
        else if(employeeScheduleDto.IsMorningSchedule())
        {
            availableHours = Availability.GetAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.MorningEndHour,
                ServiceDuration    = serviceDuration,
                AppoinmentDate     = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeProvider.Now
            });
        }
        else if(employeeScheduleDto.IsAfternoonSchedule())
        {
            availableHours = Availability.GetAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeScheduleDto.AfternoonStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppoinmentDate     = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeProvider.Now
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
