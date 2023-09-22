namespace DentallApp.Shared.Models;

/// <summary>
/// Represents an authenticated user.
/// Includes information associated with a user (user profile).
/// </summary>
public class AuthenticatedUser
{
    /// <summary>
    /// Gets or sets the authenticated user ID.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Gets or sets the ID of the personal information of an authenticated user.
    /// </summary>
    public int PersonId { get; init; }

    /// <summary>
    /// Gets or sets the full name of a user.
    /// </summary>
    public string FullName { get; init; }
}
