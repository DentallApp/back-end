namespace DentallApp.Features.Appointments.UseCases.GetAvailableHours;

public class GetAvailableHoursUseCase : IGetAvailableHoursUseCase
{
    private readonly IGetUnavailableHoursUseCase _getUnavailableHoursUseCase;
    private readonly IEmployeeScheduleRepository _employeeScheduleRepository;
    private readonly ITreatmentRepository _treatmentRepository;
    private readonly IOfficeHolidayRepository _officeHolidayRepository;
    private readonly IDateTimeService _dateTimeService;

    public GetAvailableHoursUseCase(
        IGetUnavailableHoursUseCase getUnavailableHoursUseCase,
        IEmployeeScheduleRepository employeeScheduleRepository,
        ITreatmentRepository treatmentRepository,
        IOfficeHolidayRepository officeHolidayRepository,
        IDateTimeService dateTimeService)
    {
        _getUnavailableHoursUseCase = getUnavailableHoursUseCase;
        _employeeScheduleRepository = employeeScheduleRepository;
        _treatmentRepository = treatmentRepository;
        _officeHolidayRepository = officeHolidayRepository;
        _dateTimeService = dateTimeService;
    }

    public async Task<Response<IEnumerable<AvailableTimeRangeResponse>>> ExecuteAsync(AvailableTimeRangeRequest request)
    {
        int officeId             = request.OfficeId;
        int dentistId            = request.DentistId;
        int serviceId            = request.DentalServiceId;
        var appointmentDate      = request.AppointmentDate;
        int weekDayId            = (int)appointmentDate.DayOfWeek;
        var weekDayName          = WeekDaysType.WeekDays[weekDayId];
        if (await _officeHolidayRepository.IsPublicHolidayAsync(officeId, appointmentDate.Day, appointmentDate.Month))
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(AppointmentDateIsPublicHolidayMessage);

        var employeeSchedule  = await _employeeScheduleRepository.GetByWeekDayIdAsync(dentistId, weekDayId);
        if (employeeSchedule is null || employeeSchedule.IsEmployeeScheculeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(string.Format(DentistNotAvailableMessage, weekDayName));

        if (employeeSchedule.IsOfficeScheduleDeleted || employeeSchedule.IsOfficeDeleted)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(string.Format(OfficeClosedForSpecificDayMessage, weekDayName));

        if (employeeSchedule.HasNotSchedule())
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(NoMorningOrAfternoonHoursMessage);

        var unavailables       = await _getUnavailableHoursUseCase.ExecuteAsync(dentistId, appointmentDate);
        int? treatmentDuration = await _treatmentRepository.GetDurationAsync(serviceId);
        if (treatmentDuration is null)
            return new Response<IEnumerable<AvailableTimeRangeResponse>>(DentalServiceNotAvailableMessage);

        TimeSpan serviceDuration = TimeSpan.FromMinutes(treatmentDuration.Value);
        List<AvailableTimeRangeResponse> availableHours = default;

        if (employeeSchedule.HasFullSchedule())
        {
            unavailables.Add(GetTimeOff(employeeSchedule));
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                CurrentTimeAndDate = _dateTimeService.Now,
                Unavailables       = unavailables
                    .OrderBy(timeRange => timeRange.StartHour)
                    .ThenBy(timeRange => timeRange.EndHour)
                    .ToList()
            });
        }
        else if(employeeSchedule.IsMorningSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.MorningStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.MorningEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeService.Now
            });
        }
        else if(employeeSchedule.IsAfternoonSchedule())
        {
            availableHours = Availability.CalculateAvailableHours(new AvailabilityOptions
            {
                DentistStartHour   = (TimeSpan)employeeSchedule.AfternoonStartHour,
                DentistEndHour     = (TimeSpan)employeeSchedule.AfternoonEndHour,
                ServiceDuration    = serviceDuration,
                AppointmentDate    = appointmentDate,
                Unavailables       = unavailables,
                CurrentTimeAndDate = _dateTimeService.Now
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
    private static UnavailableTimeRangeResponse GetTimeOff(EmployeeScheduleResponse employeeSchedule)
    { 
        return new()
        {
            StartHour = (TimeSpan)employeeSchedule.MorningEndHour,
            EndHour   = (TimeSpan)employeeSchedule.AfternoonStartHour
        };
    }
}
