namespace DentallApp.Features.SpecificTreatments;

public interface ISpecificTreatmentRepository
{
    Task<SpecificTreatmentRangeToPayDto> GetTreatmentWithRangeToPayAsync(int generalTreatmentId);
}
