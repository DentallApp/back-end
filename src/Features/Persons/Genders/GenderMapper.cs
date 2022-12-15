namespace DentallApp.Features.Persons.Genders;

public static class GenderMapper
{
    [Decompile]
    public static GenderGetDto MapToGenderGetDto(this Gender gender)
        => new()
        {
            Id = gender.Id,
            Name = gender.Name
        };
}
