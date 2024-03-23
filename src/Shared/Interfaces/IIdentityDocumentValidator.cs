namespace DentallApp.Shared.Interfaces;

/// <summary>
/// Represents a validator to validate identity documents.
/// </summary>
public interface IIdentityDocumentValidator
{
    /// <summary>
    /// Checks if an identity document is valid.
    /// </summary>
    /// <param name="document">The document to validate.</param>
    Result IsValid(string document);
}
