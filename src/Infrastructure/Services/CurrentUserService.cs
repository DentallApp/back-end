namespace DentallApp.Infrastructure.Services;

public class CurrentUserService(ClaimsPrincipal claimsPrincipal) : ICurrentUser
{
    public int PersonId
    {
        get
        {
            string personId = claimsPrincipal.FindFirstValue(CustomClaimsType.PersonId);
            return personId is null ?
                throw new InvalidOperationException("Claim type 'person_id' was not found.") :
                int.Parse(personId);
        }
    }

    public int UserId
    {
        get
        {
            string userId = claimsPrincipal.FindFirstValue(CustomClaimsType.UserId);
            return userId is null ?
                throw new InvalidOperationException("Claim type 'user_id' was not found.") :
                int.Parse(userId);
        }
    }

    public string UserName
    {
        get
        {
            string userName = claimsPrincipal.FindFirstValue(CustomClaimsType.UserName);
            return userName is null ?
                throw new InvalidOperationException("Claim type 'username' was not found.") :
                userName;
        }
    }
}
