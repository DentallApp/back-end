namespace DentallApp.Features.SpecificTreatments;

public interface ISpecificTreatmentService
{
    Task<IEnumerable<SpecificTreatmentGetDto>> GetSpecificTreatmentsByGeneralTreatmentIdAsync(int generalTreatmentId);
    Task<IEnumerable<SpecificTreatmentShowDto>> GetSpecificTreatmentsAsync();
    Task<Response> CreateSpecificTreatmentAsync(SpecificTreatmentInsertDto treatmentInsertDto);
    Task<Response> UpdateSpecificTreatmentAsync(int id, SpecificTreatmentUpdateDto treatmentUpdateDto);
    Task<Response> RemoveSpecificTreatmentAsync(int id);
}
