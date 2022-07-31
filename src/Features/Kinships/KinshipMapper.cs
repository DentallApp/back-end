namespace DentallApp.Features.Kinships;

public static class KinshipMapper
{
    public static KinshipGetDto MapToKinshipGetDto(this Kinship kinship)
        => new()
        {
            Id = kinship.Id,
            Name = kinship.Name
        };
}
