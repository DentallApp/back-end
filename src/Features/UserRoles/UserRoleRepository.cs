namespace DentallApp.Features.UserRoles;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }

    public void UpdateUserRoles(int userId, IOrderedEnumerable<UserRole> currentUserRoles, IOrderedEnumerable<int> newRoles)
    {
        if (currentUserRoles.Count() == newRoles.Count())
        {
            currentUserRoles.Zip(newRoles, (userRole, roleId) =>
            {
                userRole.RoleId = roleId;
                return userRole;
            }).ToList();
        }
        else
        {
            foreach (var userRole in currentUserRoles)
                if (!newRoles.Contains(userRole.RoleId))
                    Delete(userRole);

            foreach (var roleId in newRoles)
                if (!currentUserRoles.Any(userRole => userRole.RoleId == roleId))
                    Insert(new UserRole { UserId = userId, RoleId = roleId });
        }
    }
}
