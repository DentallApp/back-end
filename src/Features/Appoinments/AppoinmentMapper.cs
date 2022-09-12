namespace DentallApp.Features.Appoinments;

public static class AppoinmentMapper
{
    public static Appoinment MapToAppoinment(this AppoinmentInsertDto appoinmentInsertDto)
        => new()
        {
            UserId              = appoinmentInsertDto.UserId,
            PersonId            = appoinmentInsertDto.PersonId,
            DentistId           = appoinmentInsertDto.DentistId,
            GeneralTreatmentId  = appoinmentInsertDto.GeneralTreatmentId,
            OfficeId            = appoinmentInsertDto.OfficeId,
            Date                = appoinmentInsertDto.AppoinmentDate,
            StartHour           = appoinmentInsertDto.StartHour,
            EndHour             = appoinmentInsertDto.EndHour
        };

    public static void MapToAppoinment(this AppoinmentUpdateDto appoinmentUpdateDto, Appoinment appoinment)
    {
        appoinment.AppoinmentStatusId = appoinmentUpdateDto.StatusId;
    }

    [Decompile]
    public static UnavailableTimeRangeDto MapToUnavailableTimeRangeDto(this Appoinment appoinment)
        => new()
        {
            StartHour  = appoinment.StartHour,
            EndHour    = appoinment.EndHour
        };

    [Decompile]
    public static AppoinmentGetByDentistDto MapToAppoinmentGetByDentistDto(this Appoinment appoinment)
        => new()
        {
            AppoinmentId       = appoinment.Id,
            PatientName        = appoinment.Person.FullName,
            CreatedAt          = appoinment.CreatedAt.GetDateAndHourInSpanishFormat(),
            AppointmentDate    = appoinment.Date.GetDateInSpanishFormat(),
            StartHour          = appoinment.StartHour.GetHourWithoutSeconds(),
            EndHour            = appoinment.EndHour.GetHourWithoutSeconds(),
            DentalServiceName  = appoinment.GeneralTreatment.Name,
            Document           = appoinment.Person.Document,
            CellPhone          = appoinment.Person.CellPhone,
            Email              = appoinment.Person.Email,
            DateBirth          = appoinment.Person.DateBirth,
            Status             = appoinment.AppoinmentStatus.Name,
            StatusId           = appoinment.AppoinmentStatusId
        };

    [Decompile]
    public static AppoinmentGetByEmployeeDto MapToAppoinmentGetByEmployeeDto(this Appoinment appoinment)
        => new()
        {
            AppoinmentId       = appoinment.Id,
            PatientName        = appoinment.Person.FullName,
            CreatedAt          = appoinment.CreatedAt.GetDateAndHourInSpanishFormat(),
            AppointmentDate    = appoinment.Date.GetDateInSpanishFormat(),
            StartHour          = appoinment.StartHour.GetHourWithoutSeconds(),
            EndHour            = appoinment.EndHour.GetHourWithoutSeconds(),
            DentalServiceName  = appoinment.GeneralTreatment.Name,
            Document           = appoinment.Person.Document,
            CellPhone          = appoinment.Person.CellPhone,
            Email              = appoinment.Person.Email,
            DateBirth          = appoinment.Person.DateBirth,
            DentistId          = appoinment.DentistId,
            DentistName        = appoinment.Employee.Person.FullName,
            Status             = appoinment.AppoinmentStatus.Name,
            StatusId           = appoinment.AppoinmentStatusId
        };

    [Decompile]
    public static AppoinmentScheduledGetByEmployeeDto MapToAppoinmentScheduledGetByEmployeeDto(this Appoinment appoinment)
        => new()
        {
            AppoinmentId      = appoinment.Id,
            PatientName       = appoinment.Person.FullName,
            CreatedAt         = appoinment.CreatedAt.GetDateAndHourInSpanishFormat(),
            AppointmentDate   = appoinment.Date.GetDateInSpanishFormat(),
            StartHour         = appoinment.StartHour.GetHourWithoutSeconds(),
            EndHour           = appoinment.EndHour.GetHourWithoutSeconds(),
            DentalServiceName = appoinment.GeneralTreatment.Name,
            Document          = appoinment.Person.Document,
            CellPhone         = appoinment.Person.CellPhone,
            Email             = appoinment.Person.Email,
            DateBirth         = appoinment.Person.DateBirth,
            DentistId         = appoinment.DentistId,
            DentistName       = appoinment.Employee.Person.FullName
        };

    [Decompile]
    public static AppoinmentScheduledGetByDentistDto MapToAppoinmentScheduledGetByDentistDto(this Appoinment appoinment)
        => new()
        {
            AppoinmentId      = appoinment.Id,
            PatientName       = appoinment.Person.FullName,
            CreatedAt         = appoinment.CreatedAt.GetDateAndHourInSpanishFormat(),
            AppointmentDate   = appoinment.Date.GetDateInSpanishFormat(),
            StartHour         = appoinment.StartHour.GetHourWithoutSeconds(),
            EndHour           = appoinment.EndHour.GetHourWithoutSeconds(),
            DentalServiceName = appoinment.GeneralTreatment.Name,
            Document          = appoinment.Person.Document,
            CellPhone         = appoinment.Person.CellPhone,
            Email             = appoinment.Person.Email,
            DateBirth         = appoinment.Person.DateBirth
        };
}
