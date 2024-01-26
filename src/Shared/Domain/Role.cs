namespace DentallApp.Shared.Domain;

public class Role : BaseEntity
{
    public const int Min = 1;
    public const int Max = 3;

    public string Name { get; set; }

    public enum Predefined
    {
        Unverified = 1,
        BasicUser,
        Secretary,
        Dentist,
        Admin,
        Superadmin
    }
}
