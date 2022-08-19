namespace DentallApp.Features.SpecificTreatments;

public interface ISpecificTreatmentRepository : IRepository<SpecificTreatment>
{
    Task<IEnumerable<SpecificTreatmentGetDto>> GetSpecificTreatmentsByGeneralTreatmentIdAsync(int generalTreatmentId);
}
