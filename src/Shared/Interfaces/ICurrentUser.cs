namespace DentallApp.Shared.Interfaces;

/// <summary>
/// Represents the current user who is logged into the application.
/// </summary>
/// <remarks>
/// This interface is more general than <see cref="ICurrentEmployee"/>.
/// </remarks>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the ID of the current user's personal information.
    /// </summary>
    int PersonId { get; }

    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    int UserId { get; }

    /// <summary>
    /// Gets the user name of the current user.
    /// </summary>
    string UserName { get; }
}
