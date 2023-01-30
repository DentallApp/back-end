namespace DentallApp.Features.Chatbot.Models;

/// <summary>
/// Represents an authenticated user.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Gets or sets the authenticated user ID.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the personal information of an authenticated user.
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Gets or sets the full name of a user.
    /// </summary>
    public string FullName { get; set; }
}
