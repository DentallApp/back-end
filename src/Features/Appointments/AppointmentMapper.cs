namespace DentallApp.Features.Appointments;

public static class AppointmentMapper
{
    public static Appointment MapToAppointment(this AppointmentInsertDto appointmentInsertDto)
        => new()
        {
            UserId              = appointmentInsertDto.UserId,
            PersonId            = appointmentInsertDto.PersonId,
            DentistId           = appointmentInsertDto.DentistId,
            GeneralTreatmentId  = appointmentInsertDto.GeneralTreatmentId,
            OfficeId            = appointmentInsertDto.OfficeId,
            Date                = appointmentInsertDto.AppointmentDate,
            StartHour           = appointmentInsertDto.StartHour,
            EndHour             = appointmentInsertDto.EndHour
        };

    public static void MapToAppointment(this AppointmentUpdateDto appointmentUpdateDto, Appointment appointment)
    {
        appointment.AppointmentStatusId = appointmentUpdateDto.StatusId;
    }

    [Decompile]
    public static UnavailableTimeRangeDto MapToUnavailableTimeRangeDto(this Appointment appointment)
        => new()
        {
            StartHour  = appointment.StartHour,
            EndHour    = appointment.EndHour
        };

    [Decompile]
    public static AppointmentGetByEmployeeDto MapToAppointmentGetByEmployeeDto(this Appointment appointment)
        => new()
        {
            AppointmentId      = appointment.Id,
            PatientName        = appointment.Person.FullName,
            CreatedAt          = appointment.CreatedAt.GetDateAndHourInSpanishFormat(),
            AppointmentDate    = appointment.Date.GetDateWithStandardFormat(),
            StartHour          = appointment.StartHour.GetHourWithoutSeconds(),
            EndHour            = appointment.EndHour.GetHourWithoutSeconds(),
            DentalServiceName  = appointment.GeneralTreatment.Name,
            Document           = appointment.Person.Document,
            CellPhone          = appointment.Person.CellPhone,
            Email              = appointment.Person.Email,
            DateBirth          = appointment.Person.DateBirth,
            DentistId          = appointment.DentistId,
            DentistName        = appointment.Employee.Person.FullName,
            Status             = appointment.AppointmentStatus.Name,
            StatusId           = appointment.AppointmentStatusId,
            OfficeName         = appointment.Office.Name
        };

    [Decompile]
    public static AppointmentInfoDto MapToAppointmentInfoDto(this Appointment appointment)
        => new()
        {
            PatientName         = appointment.Person.FullName,
            CellPhone           = appointment.Person.CellPhone,
            DentistName         = appointment.Employee.Person.FullName,
            DentalServiceName   = appointment.GeneralTreatment.Name,
            OfficeName          = appointment.Office.Name
        };
}
