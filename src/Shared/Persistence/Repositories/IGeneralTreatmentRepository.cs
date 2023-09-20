namespace DentallApp.Shared.Persistence.Repositories;

public interface IGeneralTreatmentRepository
{
    Task<int?> GetDuration(int treatmentId);
}
