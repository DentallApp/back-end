namespace DentallApp.Features.AppoinmentsStatus;

public static class AppoinmentStatusMapper
{
    [Decompile]
    public static AppoinmentStatusGetDto MapToAppoinmentStatusGetDto(this AppoinmentStatus appoinmentStatus)
        => new()
        {
            Id = appoinmentStatus.Id,
            Name = appoinmentStatus.Name
        };
}
