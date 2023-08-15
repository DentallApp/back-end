﻿namespace DentallApp.Features.AppointmentReminders.Queries;

public class GetScheduledAppointmentsResponse
{
    public int AppointmentId { get; init; }
    public string PatientName { get; init; }
    public string PatientCellPhone { get; init; }
    public DateTime Date { get; init; }
    public TimeSpan StartHour { get; init; }
    public string DentistName { get; init; }
}

/// <summary>
/// Representa una consulta en la cual obtiene las citas agendadas que están próximas a la fecha estipulada.
/// </summary>
public class GetScheduledAppointmentsQuery
{
    private readonly AppDbContext _context;

    public GetScheduledAppointmentsQuery(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<GetScheduledAppointmentsResponse> Execute(int timeInAdvance, DateTime currentDate)
    {
        var appointments = _context.Set<Appointment>()
            .Where(appointment =>
                    appointment.AppointmentStatusId == AppointmentStatusId.Scheduled &&
                    _context.DateDiff(appointment.Date, currentDate) == timeInAdvance &&
                    appointment.CreatedAt != null &&
                    // Para que el recordatorio no se envíe sí el paciente agenda la cita para el día siguiente.
                    // El caso anterior sucede cuando el tiempo de antelación (timeInAdvance) es de 1 día.
                    currentDate != _context.GetDate(appointment.CreatedAt)
                  )
            .Select(appointment => new GetScheduledAppointmentsResponse
            {
                AppointmentId    = appointment.Id,
                PatientName      = appointment.Person.FullName,
                PatientCellPhone = appointment.Person.CellPhone,
                Date             = appointment.Date,
                StartHour        = appointment.StartHour,
                DentistName      = appointment.Employee.Person.FullName
            })
            .IgnoreQueryFilters()
            .ToList();

        return appointments;
    }
}