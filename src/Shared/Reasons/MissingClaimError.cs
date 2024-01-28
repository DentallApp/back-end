namespace DentallApp.Shared.Reasons;

public readonly ref struct MissingClaimError
{
    public string Message { get; }
    public MissingClaimError(string claimType)
        => Message = string.Format(Messages.MissingClaim, claimType ?? string.Empty);
}
