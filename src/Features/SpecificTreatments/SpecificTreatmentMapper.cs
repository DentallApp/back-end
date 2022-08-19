namespace DentallApp.Features.SpecificTreatments;

public static class SpecificTreatmentMapper
{
    [Decompile]
    public static SpecificTreatmentGetDto MapToSpecificTreatmentGetDto(this SpecificTreatment treatment)
        => new()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Price = treatment.Price
        };
}
