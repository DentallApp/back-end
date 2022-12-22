namespace DentallApp.Features.UserRoles;

public interface IUserRoleRepository : IRepository<UserRole>
{
    /// <summary>
    /// Adds, updates or removes the roles of a user.
    /// </summary>
    /// <param name="userId">The ID of the user to update.</param>
    /// <param name="currentUserRoles">A collection with the current roles of a user.</param>
    /// <param name="rolesId">A collection of role identifiers obtained from a web client.</param>
    void UpdateUserRoles(int userId, List<UserRole> currentUserRoles, List<int> rolesId);
}
