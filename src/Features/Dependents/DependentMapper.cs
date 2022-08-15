namespace DentallApp.Features.Dependents;

public static class DependentMapper
{
    public static Person MapToPerson(this DependentInsertDto dependentDto)
        => new()
        {
            Document  = dependentDto.Document,
            Names     = dependentDto.Names,
            LastNames = dependentDto.LastNames,
            CellPhone = dependentDto.CellPhone,
            Email     = dependentDto.Email,
            DateBirth = dependentDto.DateBirth,
            GenderId  = dependentDto.GenderId
        };

    public static Dependent MapToDependent(this DependentInsertDto dependentDto)
        => new()
        {
            KinshipId = dependentDto.KinshipId
        };

    [Decompile]
    public static DependentGetDto MapToDependentGetDto(this Dependent dependent)
        => new()
        {
            DependentId = dependent.Id,
            Document    = dependent.Person.Document,
            Names       = dependent.Person.Names,
            LastNames   = dependent.Person.LastNames,
            CellPhone   = dependent.Person.CellPhone,
            DateBirth   = dependent.Person.DateBirth,
            Email       = dependent.Person.Email,
            GenderId    = dependent.Person.GenderId,
            GenderName  = dependent.Person.Gender.Name,
            KinshipId   = dependent.KinshipId,
            KinshipName = dependent.Kinship.Name
        };

    public static void MapToPerson(this DependentUpdateDto dependentDto, Person person)
    {
        person.Names     = dependentDto.Names;
        person.LastNames = dependentDto.LastNames;
        person.CellPhone = dependentDto.CellPhone;
        person.DateBirth = dependentDto.DateBirth;
        person.GenderId  = dependentDto.GenderId;
        person.Email     = dependentDto.Email;
    }

    public static void MapToDependent(this DependentUpdateDto dependentDto, Dependent dependent)
    {
        dependent.KinshipId = dependentDto.KinshipId;
    }
}
