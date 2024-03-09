namespace DentallApp.Shared.Entities;

public class Role : BaseEntity
{
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

    /// <summary>
    /// Represents the range of roles that can be granted to an employee.
    /// </summary>
    /// <remarks>
    /// The role of super administrator is only for the owner of the business.
    /// </remarks>
    public class Range
    {
        public const int Min = (int)Predefined.Secretary;
        public const int Max = (int)Predefined.Admin;
    }
}
