namespace DentallApp.Features.Appointments.UseCases;

public class CreateAppointmentRequest
{
    /// <summary>
    /// El ID del usuario que agendó la cita.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// El ID de la persona que recibirá la atención médica.
    /// </summary>
    public int PersonId { get; set; }
    public int DentistId { get; set; }
    public int GeneralTreatmentId { get; set; }
    public int OfficeId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan EndHour { get; set; }

    [JsonIgnore]
    public SpecificTreatmentRangeToPayDto RangeToPay { get; set; }

    public Appointment MapToAppointment()
    {
        return new()
        {
            UserId             = UserId,
            PersonId           = PersonId,
            DentistId          = DentistId,
            OfficeId           = OfficeId,
            Date               = AppointmentDate,
            StartHour          = StartHour,
            EndHour            = EndHour,
            GeneralTreatmentId = GeneralTreatmentId
        };
    }
}

public class CreateAppointmentUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly SendAppointmentInformationUseCase _sendInformationUseCase;

    public CreateAppointmentUseCase(
        AppDbContext context, 
        IDateTimeProvider dateTimeProvider, 
        SendAppointmentInformationUseCase sendInformationUseCase)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
        _sendInformationUseCase = sendInformationUseCase;
    }

    public async Task<Response<InsertedIdDto>> Execute(CreateAppointmentRequest request)
    {
        // Checks if the date and time of the appointment is not available.
        bool isNotAvailable = await _context.Set<Appointment>()
            .Where(appointment =>
                  (appointment.DentistId == request.DentistId) &&
                  (appointment.Date == request.AppointmentDate) &&
                  (appointment.StartHour == request.StartHour) &&
                  (appointment.EndHour == request.EndHour) &&
                  (appointment.IsNotCanceled() ||
                   appointment.IsCancelledByEmployee ||
                   // Checks if the canceled appointment is not available.
                   // This check allows patients to choose a time slot for an appointment canceled by another basic user.
                   _dateTimeProvider.Now > _context.AddTime(_context.ToDateTime(appointment.Date), appointment.StartHour)))
            .Select(appointment => true)
            .AnyAsync();

        if (isNotAvailable)
            return new Response<InsertedIdDto>(DateAndTimeAppointmentIsNotAvailableMessage);

        var appointment = request.MapToAppointment();
        _context.Add(appointment);
        await _context.SaveChangesAsync();
        await _sendInformationUseCase.Execute(appointment.Id, request);

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = appointment.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
