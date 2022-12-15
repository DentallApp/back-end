namespace DentallApp.Features.Dependents.Kinships;

public interface IKinshipRepository
{
    Task<IEnumerable<KinshipGetDto>> GetKinshipsAsync();
}
