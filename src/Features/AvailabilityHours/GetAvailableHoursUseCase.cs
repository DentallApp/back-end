namespace DentallApp.Features.AvailabilityHours;

public class GetAvailableHoursUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;
    private readonly IGeneralTreatmentRepository _dentalServiceRepository;
    private readonly IHolidayOfficeRepository _holidayOfficeRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public GetAvailableHoursUseCase(
        IAppointmentRepository appointmentRepository,
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

    public async Task<Response<IEnumerable<AvailableTimeRangeResponse>>> Execute(AvailableTimeRangeRequest request)
    {
        int officeId             = request.OfficeId;
        int dentistId            = request.DentistId;
        int serviceId            = request.DentalServiceId;
        var appointmentDate      = request.AppointmentDate;
        int weekDayId            = (int)appointmentDate.DayOfWeek;
        var weekDayName          = WeekDaysType.WeekDays[weekDayId];
        if (await _holidayOfficeRepository.IsPublicHolidayAsync(officeId, appointmentDate.Day, appointmentDate.Month))
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(AppointmentDateIsPublicHolidayMessage);

        var employeeScheduleDto  = await _employeeScheduleRepository.GetEmployeeScheduleByWeekDayIdAsync(dentistId, weekDayId);
        if (employeeScheduleDto is null || employeeScheduleDto.IsEmployeeScheculeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(string.Format(DentistNotAvailableMessage, weekDayName));

        if (employeeScheduleDto.IsOfficeScheduleDeleted || employeeScheduleDto.IsOfficeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(string.Format(OfficeClosedForSpecificDayMessage, weekDayName));

        if (employeeScheduleDto.HasNotSchedule())
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(NoMorningOrAfternoonHoursMessage);

        var unavailables      = await _appointmentRepository.GetUnavailableHoursAsync(dentistId, appointmentDate);
        var dentalServiceDto  = await  _dentalServiceRepository.GetTreatmentWithDurationAsync(serviceId);
        if (dentalServiceDto is null)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(DentalServiceNotAvailableMessage);

        TimeSpan serviceDuration = TimeSpan.FromMinutes(dentalServiceDto.Duration);
        List<AvailableTimeRangeResponse> availableHours = null;

        if (employeeScheduleDto.HasFullSchedule())
        {
            unavailables.Add(GetTimeOff(employeeScheduleDto));
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                CurrentTimeAndDate = _dateTimeProvider.Now,
                Unavailables       = unavailables
                    .OrderBy(timeRange => timeRange.StartHour)
                    .ThenBy(timeRange => timeRange.EndHour)
                    .ToList()
            });
        }
        else if(employeeScheduleDto.IsMorningSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeScheduleDto.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.MorningEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeProvider.Now
            });
        }
        else if(employeeScheduleDto.IsAfternoonSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeScheduleDto.AfternoonStartHour,
                DentistEndHour     = (TimeSpan)employeeScheduleDto.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeProvider.Now
            });
        }

        if(availableHours is null)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(NoSchedulesAvailableMessage);

        return new Response<IEnumerable<AvailableTimeRangeResponse>>
        {
            Success = true,
            Data    = availableHours,
            Message = GetResourceMessage
        };
    }

    /// <summary>
    /// Obtiene el tiempo libre (o punto de descanso) del empleado. 
    /// Este tiempo se descarta para el cálculo de los horarios disponibles.
    /// </summary>
    private static UnavailableTimeRangeResponse GetTimeOff(EmployeeScheduleByWeekDayDto employeeScheduleDto)
    { 
        return new()
        {
            StartHour = (TimeSpan)employeeScheduleDto.MorningEndHour,
            EndHour   = (TimeSpan)employeeScheduleDto.AfternoonStartHour
        };
    }
}
