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

    public static GeneralTreatment MapToGeneralTreatmentInsertDto(this GeneralTreatmentInsertDto treatmentInsertDto)
        => new()
        {
            Name = treatmentInsertDto.Name,
            Description = treatmentInsertDto.Description,
            Duration = treatmentInsertDto.Duration,
            ImageUrl = treatmentInsertDto.Image.GetNewPathForDentalServiceImage()
        };
}
