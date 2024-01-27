namespace DentallApp.Shared.Reasons;

public class MissingClaimError
{
    public string Message { get; }
    public MissingClaimError(string claimType)
        => Message = string.Format(Messages.MissingClaim, claimType ?? string.Empty);
}
