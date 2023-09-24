namespace DentallApp.Shared.Persistence.Repositories;

public interface ITreatmentRepository
{
    Task<int?> GetDurationAsync(int generalTreatmentId);
    Task<PayRange> GetRangeToPayAsync(int generalTreatmentId);
}
