namespace DentallApp.Features.Offices;

public static class OfficeMapper
{
    [Decompile]
    public static OfficeGetDto MapToOfficeGetDto(this Office office)
        => new()
        {
            Id = office.Id,
            Name = office.Name
        };
}
