namespace DentallApp.Shared.Persistence.Repositories;

public interface ITreatmentRepository
{
    Task<int?> GetDuration(int generalTreatmentId);
    Task<PayRange> GetRangeToPay(int generalTreatmentId);
}
