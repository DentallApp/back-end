namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentService
{
    Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatments();
    Task<Response<GeneralTreatmentGetDto>> GetTreatmentById(int id);
}
