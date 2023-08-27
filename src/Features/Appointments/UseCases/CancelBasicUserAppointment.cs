namespace DentallApp.Features.Appointments.UseCases;

public class CancelBasicUserAppointmentUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CancelBasicUserAppointmentUseCase(AppDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response> Execute(int appointmentId, int currentUserId)
    {
        var appointment = await _context.Set<Appointment>()
            .Where(appointment => appointment.Id == appointmentId)
            .FirstOrDefaultAsync();

        if (appointment is null)
            return new Response(ResourceNotFoundMessage);

        if (appointment.UserId != currentUserId)
            return new Response(AppointmentNotAssignedMessage);

        if (_dateTimeProvider.Now > (appointment.Date + appointment.StartHour))
            return new Response(AppointmentThatHasAlreadyPassedBasicUserMessage);

        appointment.AppointmentStatusId = AppointmentStatusId.Canceled;
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
