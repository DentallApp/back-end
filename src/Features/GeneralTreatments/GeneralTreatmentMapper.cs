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

    [Decompile]
    public static GeneralTreatmentShowDto MapToGeneralTreatmentShowDto(this GeneralTreatment treatment)
        => new()
        {
            Id = treatment.Id,
            Name = treatment.Name,
            Description = treatment.Description,
            Duration = treatment.Duration
        };

    public static GeneralTreatment MapToGeneralTreatment(this GeneralTreatmentInsertDto treatmentInsertDto)
        => new()
        {
            Name = treatmentInsertDto.Name,
            Description = treatmentInsertDto.Description,
            Duration = treatmentInsertDto.Duration,
            ImageUrl = treatmentInsertDto.Image.GetNewPathForDentalServiceImage()
        };

    public static void MapToGeneralTreatment(this GeneralTreatmentUpdateDto treatmentUpdateDto, GeneralTreatment treatment)
    {
        treatment.Name = treatmentUpdateDto.Name;
        treatment.Description = treatmentUpdateDto.Description;
        treatment.Duration = treatmentUpdateDto.Duration;
        treatment.ImageUrl = treatmentUpdateDto.Image is null ? treatment.ImageUrl : treatmentUpdateDto.Image.GetNewPathForDentalServiceImage();
    }
}
