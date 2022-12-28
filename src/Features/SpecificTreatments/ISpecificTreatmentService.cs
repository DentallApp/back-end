namespace DentallApp.Features.SpecificTreatments;

public interface ISpecificTreatmentService
{
    Task<Response<DtoBase>> CreateSpecificTreatmentAsync(SpecificTreatmentInsertDto treatmentInsertDto);
    Task<Response> UpdateSpecificTreatmentAsync(int id, SpecificTreatmentUpdateDto treatmentUpdateDto);
    Task<Response> RemoveSpecificTreatmentAsync(int id);
}
