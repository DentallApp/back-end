namespace DentallApp.Features.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;  
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

    public async Task<Response> ChangePasswordAsync(int userId, UserUpdatePasswordDto userUpdatePasswordDto)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (user is null)
            return new Response(UsernameNotFoundMessage);

        if (!_passwordHasher.Verify(userUpdatePasswordDto.OldPassword, user.Password))
            return new Response(OldPasswordIncorrectMessage);

        user.Password = _passwordHasher.HashPassword(userUpdatePasswordDto.NewPassword);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = PasswordSuccessfullyResetMessage
        };
    }
}
