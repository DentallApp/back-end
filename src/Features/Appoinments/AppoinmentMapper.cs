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

    [Decompile]
    public static UnavailableTimeRangeDto MapToUnavailableTimeRangeDto(this Appoinment appoinment)
        => new()
        {
            StartHour  = appoinment.StartHour,
            EndHour    = appoinment.EndHour
        };
}
