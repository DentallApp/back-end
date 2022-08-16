namespace DentallApp.Features.Dependents;

public interface IDependentRepository : ISoftDeleteRepository<Dependent>
{
    Task<IEnumerable<DependentGetDto>> GetDependentsByUserIdAsync(int userId);
    Task<Dependent> GetDependentByIdAsync(int id);
}
