﻿namespace Plugin.AppointmentReminders;

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
/// Represents a query that retrieves scheduled appointments that are close to the stipulated date.
/// </summary>
public class GetScheduledAppointmentsQuery(DbContext context)
{
    public IEnumerable<GetScheduledAppointmentsResponse> Execute(int timeInAdvance, DateTime currentDate)
    {
        var appointments = context.Set<Appointment>()
            .Where(appointment =>
                    appointment.AppointmentStatusId == (int)AppointmentStatus.Predefined.Scheduled &&
                    DBFunctions.DateDiff(appointment.Date, currentDate) == timeInAdvance &&
                    appointment.CreatedAt != null &&
                    // So that the reminder is not sent if the patient schedules the appointment for the next day.
                    // The previous case occurs when the time in advance is one day.
                    currentDate != DBFunctions.GetDate(appointment.CreatedAt)
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
            .AsNoTracking()
            .ToList();

        return appointments;
    }
}
