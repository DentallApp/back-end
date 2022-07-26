namespace DentallApp.Features.Users;

public static class UserMapper
{
    private static FullUserProfileDto MapToFullUserProfileDto(FullUserProfileDto userProfile, User user)
    {
        userProfile.Document    = user.Person.Document;
        userProfile.Names       = user.Person.Names;
        userProfile.LastNames   = user.Person.LastNames;
        userProfile.CellPhone   = user.Person.CellPhone;
        userProfile.DateBirth   = user.Person.DateBirth;
        userProfile.GenderName  = user.Person.Gender.Name;
        userProfile.UserId      = user.Id;
        userProfile.UserName    = user.UserName;
        userProfile.Roles       = user.UserRoles.Select(role => role.Role.Name);
        return userProfile;
    }

    public static FullUserProfileDto MapToUserProfileDto(this User user)
        => MapToFullUserProfileDto(new FullUserProfileDto(), user);


    public static UserLoginDto MapToUserLoginDto(this User user)
        => MapToFullUserProfileDto(new UserLoginDto(), user) as UserLoginDto;

    public static UserClaims MapToUserClaims(this UserLoginDto userLoginDto)
        => new()
        {
            UserId = userLoginDto.UserId,
            UserName = userLoginDto.UserName,
            FullName = $"{userLoginDto.LastNames} {userLoginDto.Names}",
            Roles = userLoginDto.Roles
        };

    public static UserClaims MapToUserClaims(this UserInsertDto userInsertDto, int userId)
        => new()
        {
            UserId = userId,
            UserName = userInsertDto.UserName,
            FullName = $"{userInsertDto.LastNames} {userInsertDto.Names}",
            Roles = new List<string> { RolesName.Unverified }
        };

    public static Person MapToPerson(this UserInsertDto userInsertDto)
        => new()
        {
            Document = userInsertDto.Document,
            Names = userInsertDto.Names,
            LastNames = userInsertDto.LastNames,
            CellPhone = userInsertDto.CellPhone,
            Email = userInsertDto.UserName,
            DateBirth = userInsertDto.DateBirth,
            GenderId = userInsertDto.GenderId
        };

    public static User MapToUser(this UserInsertDto userInsertDto)
        => new()
        {
            UserName = userInsertDto.UserName,
            Password = userInsertDto.Password
        };

    public static UserResetPasswordDto MapToUserResetPasswordDto(this User user)
        => new()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Name = user.Person.Names,
            Password = user.Password
        };
}
