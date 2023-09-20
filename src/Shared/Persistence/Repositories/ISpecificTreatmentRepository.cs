namespace DentallApp.Shared.Persistence.Repositories;

public interface ISpecificTreatmentRepository
{
    Task<RangeToPayResponse> GetRangeToPay(int generalTreatmentId);
}
