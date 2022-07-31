namespace DentallApp.Features.Kinships;

public interface IKinshipRepository
{
    Task<IEnumerable<KinshipGetDto>> GetKinshipsAsync();
}
