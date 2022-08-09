namespace DentallApp.Features.GeneralTreatments;

public static class GeneralTreatmentMapper
{
    [Decompile]
    public static GeneralTreatmentGetDto MapToGeneralTreatmentGetDto(this GeneralTreatment treatment)
        => new()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Description = treatment.Description,
            ImageUrl = treatment.ImageUrl
        };
}
