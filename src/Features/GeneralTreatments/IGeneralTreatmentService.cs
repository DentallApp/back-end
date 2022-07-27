namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentService
{
    Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync();
    Task<Response<GeneralTreatmentGetDto>> GetTreatmentByIdAsync(int id);
}
