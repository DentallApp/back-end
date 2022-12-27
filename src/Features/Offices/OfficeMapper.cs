namespace DentallApp.Features.Offices;

public static class OfficeMapper
{
    [Decompile]
    public static OfficeGetDto MapToOfficeGetDto(this Office office)
        => new()
        {
            Id   = office.Id,
            Name = office.Name
        };

    [Decompile]
    public static OfficeShowDto MapToOfficeShowDto(this Office office)
        => new()
        {
            Id               = office.Id,
            Name             = office.Name,
            Address          = office.Address,
            ContactNumber    = office.ContactNumber,
            IsDeleted        = office.IsDeleted,
            IsCheckboxTicked = office.IsCheckboxTicked
        };

    public static Office MapToOffice(this OfficeInsertDto officeInsertDto)
        => new()
        {
            Name            = officeInsertDto.Name,
            Address         = officeInsertDto.Address,
            ContactNumber   = officeInsertDto.ContactNumber
        };

    public static void MapToOffice(this OfficeUpdateDto officeUpdateDto, Office office)
    {
        office.Name             = officeUpdateDto.Name;
        office.Address          = officeUpdateDto.Address;
        office.ContactNumber    = officeUpdateDto.ContactNumber;
        office.IsDeleted        = officeUpdateDto.IsDeleted;
        office.IsCheckboxTicked = officeUpdateDto.IsCheckboxTicked;
    }
}
