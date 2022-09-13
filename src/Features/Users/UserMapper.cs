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
        userProfile.GenderId    = user.Person.GenderId;
        userProfile.UserId      = user.Id;
        userProfile.PersonId    = user.PersonId;
        userProfile.UserName    = user.UserName;
        userProfile.Roles       = user.UserRoles.OrderBy(role => role.RoleId)
                                                .Select(role => role.Role.Name);
        return userProfile;
    }

    public static FullUserProfileDto MapToUserProfileDto(this User user)
        => MapToFullUserProfileDto(new FullUserProfileDto(), user);


    public static UserLoginDto MapToUserLoginDto(this User user)
        => MapToFullUserProfileDto(new UserLoginDto(), user) as UserLoginDto;

    public static EmployeeLoginDto MapToEmployeeLoginDto(this User user)
        => MapToFullUserProfileDto(new EmployeeLoginDto(), user) as EmployeeLoginDto;

    public static UserClaims MapToUserClaims(this UserLoginDto userLoginDto)
        => new()
        {
            UserId = userLoginDto.UserId,
            PersonId = userLoginDto.PersonId,
            UserName = userLoginDto.UserName,
            FullName = userLoginDto.FullName,
            Roles = userLoginDto.Roles
        };

    public static UserClaims MapToUserClaims(this UserInsertDto userInsertDto, User user)
        => new()
        {
            UserId = user.Id,
            PersonId = user.PersonId,
            UserName = userInsertDto.UserName,
            FullName = userInsertDto.FullName,
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

    [Decompile]
    public static UserResetPasswordDto MapToUserResetPasswordDto(this User user)
        => new()
        {
            UserId = user.Id,
            UserName = user.UserName,
            Name = user.Person.Names,
            Password = user.Password
        };

    public static void MapToPerson(this UserUpdateDto userUpdateDto, Person person)
    {
        person.Names = userUpdateDto.Names;
        person.LastNames = userUpdateDto.LastNames;
        person.CellPhone = userUpdateDto.CellPhone;
        person.DateBirth = userUpdateDto.DateBirth;
        person.GenderId = userUpdateDto.GenderId;
    }
}
