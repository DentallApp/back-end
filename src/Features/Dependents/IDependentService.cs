namespace DentallApp.Features.Dependents;

public interface IDependentService
{
    Task<Response> CreateDependentAsync(int userId, DependentInsertDto dependentDto);
    Task<IEnumerable<DependentGetDto>> GetDependentsByUserIdAsync(int userId);
    Task<Response> RemoveDependentAsync(int dependentId, int userId);
    Task<Response> UpdateDependentAsync(int dependentId, int userId, DependentUpdateDto dependentDto);
}
