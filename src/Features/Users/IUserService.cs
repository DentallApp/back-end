namespace DentallApp.Features.Users;

public interface IUserService
{
    Task<Response> EditUserProfileAsync(int personId, UserUpdateDto userUpdateDto);
    Task<Response> ChangePasswordAsync(int userId, UserUpdatePasswordDto userUpdatePasswordDto);
}
