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

    [Decompile]
    public static SpecificTreatmentShowDto MapToSpecificTreatmentShowDto(this SpecificTreatment specificTreatment)
        => new()
        {
            SpecificTreatmentId = specificTreatment.Id,
            SpecificTreatmentName = specificTreatment.Name,
            GeneralTreatmentId = specificTreatment.GeneralTreatmentId,
            GeneralTreatmentName = specificTreatment.GeneralTreatment.Name,
            Price = specificTreatment.Price
        };

    public static SpecificTreatment MapToSpecificTreatment(this SpecificTreatmentInsertDto treatmentInsertDto)
        => new()
        {
            Name = treatmentInsertDto.Name,
            GeneralTreatmentId = treatmentInsertDto.GeneralTreatmentId,
            Price = treatmentInsertDto.Price
        };

    public static void MapToSpecificTreatment(this SpecificTreatmentUpdateDto treatmentUpdateDto, SpecificTreatment specificTreatment)
    {
        specificTreatment.Name = treatmentUpdateDto.Name;
        specificTreatment.GeneralTreatmentId = treatmentUpdateDto.GeneralTreatmentId;
        specificTreatment.Price = treatmentUpdateDto.Price;
    }
}
