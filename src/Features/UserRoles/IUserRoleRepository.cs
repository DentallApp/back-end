namespace DentallApp.Features.UserRoles;

public interface IUserRoleRepository : IRepository<UserRole>
{
    void UpdateUserRoles(int userId, IOrderedEnumerable<UserRole> currentUserRoles, IOrderedEnumerable<int> newRoles);
}
