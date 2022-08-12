namespace DentallApp.Features.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> EditUserProfileAsync(int personId, UserUpdateDto userUpdateDto)
    {
        var data = await _unitOfWork.PersonRepository.GetByIdAsync(personId);
        if (data is null)
            return new Response(UsernameNotFoundMessage);

        userUpdateDto.MapToPerson(data);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
