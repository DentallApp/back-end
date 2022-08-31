namespace DentallApp.Features.UserRoles;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }

    public void UpdateUserRoles(int userId, IOrderedEnumerable<UserRole> currentUserRoles, IOrderedEnumerable<int> newRoles)
    {
        if (currentUserRoles.Count() == newRoles.Count())
        {
            currentUserRoles.Zip(newRoles, (currentUserRole, newRoleId) =>
            {
                currentUserRole.RoleId = newRoleId;
                return currentUserRole;
            }).ToList();
        }
        else
        {
            foreach (UserRole currentUserRole in currentUserRoles)
                if (!newRoles.Contains(currentUserRole.RoleId))
                    Delete(currentUserRole);

            foreach (int newRoleId in newRoles)
                if (!currentUserRoles.Any(currentUserRole => currentUserRole.RoleId == newRoleId))
                    Insert(new UserRole { UserId = userId, RoleId = newRoleId });
        }
    }
}
