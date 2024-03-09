namespace DentallApp.Shared.Exceptions;

public class ClaimNotFoundException : Exception
{
    public ClaimNotFoundException(string claimType) 
        : base($"Claim type '{claimType}' was not found.") 
    { 
    }
}
