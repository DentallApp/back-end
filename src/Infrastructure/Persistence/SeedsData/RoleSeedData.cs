namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class RoleSeedData
{
    public static ModelBuilder CreateDefaultRoles(this ModelBuilder builder)
    {
         builder.AddSeedData(
            new Role
            {
                Id = 1,
                Name = RoleName.Unverified
            },
            new Role
            {
                Id = 2,
                Name = RoleName.BasicUser
            },
            new Role
            {
                Id = 3,
                Name = RoleName.Secretary
            },
            new Role
            {
                Id = 4,
                Name = RoleName.Dentist
            },
            new Role
            {
                Id = 5,
                Name = RoleName.Admin
            },
            new Role
            {
                Id = 6,
                Name = RoleName.Superadmin
            }
        );
        return builder;
    }
}
