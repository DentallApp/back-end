namespace DentallApp.Features.Appointments.UseCases;

public class UpdateAppointmentRequest
{
    public int StatusId { get; init; }
}

public class UpdateAppointmentUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeService _dateTimeService;

    public UpdateAppointmentUseCase(AppDbContext context, IDateTimeService dateTimeService)
    {
        _context = context;
        _dateTimeService = dateTimeService;
    }

    public async Task<Response> Execute(int id, ClaimsPrincipal currentEmployee, UpdateAppointmentRequest request)
    {
        var appointment = await _context.Set<Appointment>()
            .Where(appointment => appointment.Id == id)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return new Response(ResourceNotFoundMessage);

        if (appointment.AppointmentStatusId == AppointmentStatusId.Canceled)
            return new Response(AppointmentIsAlreadyCanceledMessage);

        if (_dateTimeService.Now.Date > appointment.Date)
            return new Response(AppointmentCannotBeUpdatedForPreviousDaysMessage);

        if (currentEmployee.IsOnlyDentist() && appointment.DentistId != currentEmployee.GetEmployeeId())
            return new Response(AppointmentNotAssignedMessage);

        if (currentEmployee.IsNotInOffice(appointment.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        appointment.AppointmentStatusId = request.StatusId;
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
