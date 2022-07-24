namespace DentallApp.Features.Roles;

public static class RoleChecker
{
    public static bool IsUnverified(this User user)
        => user.UserRoles.First().RoleId == RolesId.Unverified;

    public static bool IsVerified(this User user)
        => !IsUnverified(user);
}
