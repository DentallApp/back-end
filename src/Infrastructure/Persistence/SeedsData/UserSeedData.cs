namespace DentallApp.Infrastructure.Persistence.SeedsData;

public static class UserSeedData
{
    private const string BasicUserEmail  = "basic_user@hotmail.com";
    private const string SecretaryEmail  = "secretary@hotmail.com";
    private const string DentistEmail    = "dentist@hotmail.com";
    private const string AdminEmail      = "admin@hotmail.com";
    private const string SuperAdminEmail = "superadmin@hotmail.com";
    private const string Password        = "$2a$10$60QnEiafBCLfVBMfQkExVeolyBxVHWcSQKTvkxVJj9FUozRpRP/GW";

    public static void CreateDefaultUserAccounts(this ModelBuilder builder)
    {
        CreateBasicUserAccount(builder);
        CreateSecretaryAccount(builder);
        CreateDentistAccount(builder);
        CreateAdminAccount(builder);
        CreateSuperAdminAccount(builder);
        CreateAdditionalAccounts(builder);
    }

    private static void CreateBasicUserAccount(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 1,
                        Document = "0923611701",
                        Names = "Roberto Emilio",
                        LastNames = "Placencio Pinto",
                        CellPhone = "0998994332",
                        Email = BasicUserEmail,
                        DateBirth = new DateTime(1999, 08, 27),
                        GenderId = 1
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 1,
                        UserName = BasicUserEmail,
                        Password = Password,
                        PersonId = 1
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 1,
                        UserId = 1,
                        RoleId = (int)Role.Predefined.BasicUser
                    }
               );
    }

    private static void CreateSecretaryAccount(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 2,
                        Document = "0923611702",
                        Names = "María Consuelo",
                        LastNames = "Rodríguez Valencia",
                        CellPhone = "0998994333",
                        Email = SecretaryEmail,
                        DateBirth = new DateTime(1999, 07, 25),
                        GenderId = 2
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 2,
                        UserName = SecretaryEmail,
                        Password = Password,
                        PersonId = 2
                    }
               );
        builder.AddSeedData(
                    new Employee
                    {
                        Id = 1,
                        UserId = 2,
                        PersonId = 2,
                        OfficeId = 1,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 2,
                        UserId = 2,
                        RoleId = (int)Role.Predefined.Secretary
                    }
               );
    }

    private static void CreateDentistAccount(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 3,
                        Document = "0923611703",
                        Names = "Guillermo Oswaldo",
                        LastNames = "Rodríguez Rivera",
                        CellPhone = "0998994334",
                        Email = DentistEmail,
                        DateBirth = new DateTime(1999, 07, 21),
                        GenderId = 1
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 3,
                        UserName = DentistEmail,
                        Password = Password,
                        PersonId = 3
                    }
               );
        builder.AddSeedData(
                    new Employee
                    {
                        Id = 2,
                        UserId = 3,
                        PersonId = 3,
                        OfficeId = 1,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 3,
                        UserId = 3,
                        RoleId = (int)Role.Predefined.Dentist
                    }
               );
    }

    private static void CreateAdminAccount(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 4,
                        Document = "0923611704",
                        Names = "Jean Carlos",
                        LastNames = "Figueroa Lopéz",
                        CellPhone = "0998994335",
                        Email = AdminEmail,
                        DateBirth = new DateTime(1999, 09, 15),
                        GenderId = 1
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 4,
                        UserName = AdminEmail,
                        Password = Password,
                        PersonId = 4
                    }
               );
        builder.AddSeedData(
                    new Employee
                    {
                        Id = 3,
                        UserId = 4,
                        PersonId = 4,
                        OfficeId = 1,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 4,
                        UserId = 4,
                        RoleId = (int)Role.Predefined.Admin
                    }
               );
    }

    private static void CreateSuperAdminAccount(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 5,
                        Document = "0923611705",
                        Names = "David Sebastian",
                        LastNames = "Román Amariles",
                        CellPhone = "0998994336",
                        Email = SuperAdminEmail,
                        DateBirth = new DateTime(1999, 08, 27),
                        GenderId = 1
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 5,
                        UserName = SuperAdminEmail,
                        Password = Password,
                        PersonId = 5
                    }
               );
        builder.AddSeedData(
                    new Employee
                    {
                        Id = 4,
                        UserId = 5,
                        PersonId = 5,
                        OfficeId = 1,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 5,
                        UserId = 5,
                        RoleId = (int)Role.Predefined.Superadmin
                    }
               );
    }

    private static void CreateAdditionalAccounts(ModelBuilder builder)
    {
        builder.AddSeedData(
                    new Person
                    {
                        Id = 6,
                        Document = "0923611706",
                        Names = "María José",
                        LastNames = "Amariles Valencia",
                        CellPhone = "0998994337",
                        Email = "mary_01@hotmail.com",
                        DateBirth = new DateTime(1999, 01, 10),
                        GenderId = 2
                    },
                    new Person
                    {
                        Id = 7,
                        Document = "0923611707",
                        Names = "Carlos Andrés",
                        LastNames = "Torres Rivera",
                        CellPhone = "0998994338",
                        Email = "torres_02@hotmail.com",
                        DateBirth = new DateTime(1998, 02, 07),
                        GenderId = 1
                    }
               );
        builder.AddSeedData(
                    new User
                    {
                        Id = 6,
                        UserName = "mary_01@hotmail.com",
                        Password = Password,
                        PersonId = 6
                    },
                    new User
                    {
                        Id = 7,
                        UserName = "torres_02@hotmail.com",
                        Password = Password,
                        PersonId = 7
                    }
               );
        builder.AddSeedData(
                    new Employee
                    {
                        Id = 5,
                        UserId = 6,
                        PersonId = 6,
                        OfficeId = 2,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    },
                    new Employee
                    {
                        Id = 6,
                        UserId = 7,
                        PersonId = 7,
                        OfficeId = 3,
                        PregradeUniversity = "UG",
                        PostgradeUniversity = "ESPOL"
                    }
               );
        builder.AddSeedData(
                    new UserRole
                    {
                        Id = 6,
                        UserId = 6,
                        RoleId = (int)Role.Predefined.Dentist
                    },
                    new UserRole
                    {
                        Id = 7,
                        UserId = 7,
                        RoleId = (int)Role.Predefined.Dentist
                    }
               );
    }
}
