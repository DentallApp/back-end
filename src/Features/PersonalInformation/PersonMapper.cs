namespace DentallApp.Features.PersonalInformation;

public static class PersonMapper
{
    [Decompile]
    public static PersonGetDto MapToPersonGetDto(this Person person)
        => new()
        {
            PersonId    = person.Id,
            Document    = person.Document,
            Names       = person.Names,
            LastNames   = person.LastNames,
            CellPhone   = person.CellPhone,
            Email       = person.Email
        };

    public static Person MapToPerson(this PersonInsertDto personInsertDto)
        => new()
        {
            Document  = personInsertDto.Document,
            Names     = personInsertDto.Names,
            LastNames = personInsertDto.LastNames,
            DateBirth = personInsertDto.DateBirth,
            GenderId  = personInsertDto.GenderId,
            CellPhone = personInsertDto.CellPhone,
            Email     = personInsertDto.Email
        };
}
