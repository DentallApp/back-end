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
            Price = specificTreatment.Price,
        };
}
