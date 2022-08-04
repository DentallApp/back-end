namespace DentallApp.Features.Dependents;

public class DependentService : IDependentService
{
    private readonly IUnitOfWork _unitOfWork;

    public DependentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> CreateDependentAsync(int userId, DependentInsertDto dependentDto)
    {
        var person = dependentDto.MapToPerson();
        _unitOfWork.PersonRepository.Insert(person);
        var dependent = dependentDto.MapToDependent();
        _unitOfWork.DependentRepository.Insert(dependent);
        dependent.UserId = userId;
        dependent.Person = person;
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<IEnumerable<DependentGetDto>> GetDependentsByUserIdAsync(int userId)
        => await _unitOfWork.DependentRepository.GetDependentsByUserIdAsync(userId);

    public async Task<Response> RemoveDependentAsync(int dependentId, int userId)
    {
        var dependent = await _unitOfWork.DependentRepository.GetByIdAsync(dependentId);
        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        dependent.StatusId = StatusId.Inactive;
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> UpdateDependentAsync(int dependentId, int userId, DependentUpdateDto dependentDto)
    {
        var dependent = await _unitOfWork.DependentRepository.GetDependentByIdAsync(dependentId);
        if (dependent is null)
            return new Response(ResourceNotFoundMessage);

        if (dependent.UserId != userId)
            return new Response(ResourceFromAnotherUserMessage);

        dependentDto.MapToPerson(dependent.Person);
        dependentDto.MapToDependent(dependent);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
