namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentRepository : IRepository<GeneralTreatment>
{
    Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatments();
}
