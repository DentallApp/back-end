namespace DentallApp.Shared.Persistence.Repositories;

public interface ISpecificTreatmentRepository
{
    Task<PayRange> GetRangeToPay(int generalTreatmentId);
}
