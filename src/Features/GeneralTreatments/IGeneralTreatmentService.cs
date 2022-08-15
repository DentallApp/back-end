namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentService
{
    Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync();
    Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsWithoutImageUrlAsync();
    Task<Response<GeneralTreatmentGetDto>> GetTreatmentByIdAsync(int id);
    Task<Response> CreateTreatmentAsync(GeneralTreatmentInsertDto treatmentInsertDto);
    Task<Response> UpdateTreatmentAsync(int id, GeneralTreatmentUpdateDto treatmentUpdateDto);
    Task<Response> RemoveTreatmentAsync(int id);
}
