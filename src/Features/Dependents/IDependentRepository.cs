namespace DentallApp.Features.Dependents;

public interface IDependentRepository : IRepository<Dependent>
{
    Task<IEnumerable<DependentGetDto>> GetDependentsByUserIdAsync(int userId);
    Task<Dependent> GetDependentByIdAsync(int id);
}
