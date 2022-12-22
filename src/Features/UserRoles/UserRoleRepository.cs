namespace DentallApp.Features.UserRoles;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public void UpdateUserRoles(int userId, List<UserRole> currentUserRoles, List<int> rolesId)
        => this.AddOrUpdateOrDelete(key: userId, source: ref currentUserRoles, identifiers: ref rolesId);
}
