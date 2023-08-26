namespace DentallApp.Features.Appointments.UseCases;

public class UpdateAppointmentRequest
{
    public int StatusId { get; init; }
}

public class UpdateAppointmentUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateAppointmentUseCase(AppDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
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

        if (_dateTimeProvider.Now.Date > appointment.Date)
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
