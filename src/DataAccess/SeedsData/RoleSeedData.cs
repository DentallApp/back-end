namespace DentallApp.DataAccess.SeedsData;

public static class RoleSeedData
{
    public static ModelBuilder CreateDefaultRoles(this ModelBuilder builder)
    {
         builder.AddSeedData(
            new Role
            {
                Id = RolesId.Unverified,
                Name = RolesName.Unverified
            },
            new Role
            {
                Id = RolesId.BasicUser,
                Name = RolesName.BasicUser
            },
            new Role
            {
                Id = RolesId.Secretary,
                Name = RolesName.Secretary
            },
            new Role
            {
                Id = RolesId.Dentist,
                Name = RolesName.Dentist
            },
            new Role
            {
                Id = RolesId.Admin,
                Name = RolesName.Admin
            },
            new Role
            {
                Id = RolesId.Superadmin,
                Name = RolesName.Superadmin
            }
        );
        return builder;
    }
}
