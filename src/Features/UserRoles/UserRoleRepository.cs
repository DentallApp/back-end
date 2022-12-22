namespace DentallApp.Features.UserRoles;

public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public void UpdateUserRoles(int userId, List<UserRole> currentUserRoles, List<int> rolesId)
        => this.UpdateEntities(key: userId, source: ref currentUserRoles, newEntries: ref rolesId);
}
