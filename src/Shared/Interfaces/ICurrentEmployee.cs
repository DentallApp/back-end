namespace DentallApp.Shared.Interfaces;

/// <summary>
/// Represents the current employee who is logged into the application.
/// </summary>
/// <remarks>
/// This interface is more specific than <see cref="ICurrentUser"/>.
/// </remarks>
public interface ICurrentEmployee
{
    /// <summary>
    /// Gets the ID of the office where the current employee is located.
    /// </summary>
    int OfficeId { get; }

    /// <summary>
    /// Gets the ID of the current employee.
    /// </summary>
    int EmployeeId { get; }

    /// <summary>
    /// Gets the user name of the current employee.
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// Checks if the current employee is not in a specified office.
    /// </summary>
    /// <param name="officeId">The ID of an office.</param>
    /// <returns>
    /// true if the current employee is not in the specified office, otherwise false.
    /// </returns>
    bool IsNotInOffice(int officeId);

    /// <summary>
    /// Checks if the current employee has the role of super administrator.
    /// </summary>
    /// <returns>true if it is an super administrator, otherwise false.</returns>
    bool IsSuperAdmin();

    /// <summary>
    /// Checks if the current employee has the role of administrator.
    /// </summary>
    /// <returns>true if it is an administrator, otherwise false.</returns>
    bool IsAdmin();

    /// <summary>
    /// Checks if the current employee has the role of dentist.
    /// </summary>
    /// <returns>true if it is an dentist, otherwise false.</returns>
    bool IsDentist();

    /// <summary>
    /// Checks if the current employee has only the role of dentist.
    /// </summary>
    /// <returns>true if it has only the role of dentist, otherwise false.</returns>
    bool IsOnlyDentist();

    /// <summary>
    /// Checks if the current employee does not have permissions to grant the new roles.
    /// </summary>
    /// <remarks>
    /// These roles can only be given to another employee: 
    /// <list type="bullet">
    /// <item>Secretary</item>
    /// <item>Dentist</item>
    /// <item>Admin</item>
    /// </list>
    /// The role of super administrator is only for the owner of the business.
    /// </remarks>
    /// <param name="rolesId">A set of new roles to be granted.</param>
    /// <returns>
    /// true if the current employee has no permissions, otherwise false.
    /// </returns>
    bool HasNotPermissions(IEnumerable<int> rolesId);
}
