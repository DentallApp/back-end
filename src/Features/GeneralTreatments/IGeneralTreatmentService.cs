namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentService
{
    Task<Response<GeneralTreatmentGetDto>> GetTreatmentByIdAsync(int id);
    Task<Response<DtoBase>> CreateTreatmentAsync(GeneralTreatmentInsertDto treatmentInsertDto);
    Task<Response> UpdateTreatmentAsync(int id, GeneralTreatmentUpdateDto treatmentUpdateDto);
    Task<Response> RemoveTreatmentAsync(int id);
}
