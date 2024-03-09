namespace DentallApp.Infrastructure.Services;

public class CurrentUserService(ClaimsPrincipal claimsPrincipal) : ICurrentUser
{
    public int PersonId
    {
        get
        {
            string personId = claimsPrincipal.FindFirstValue(CustomClaimsType.PersonId);
            return personId is null ?
                throw new ClaimNotFoundException(CustomClaimsType.PersonId) :
                int.Parse(personId);
        }
    }

    public int UserId
    {
        get
        {
            string userId = claimsPrincipal.FindFirstValue(CustomClaimsType.UserId);
            return userId is null ?
                throw new ClaimNotFoundException(CustomClaimsType.UserId) :
                int.Parse(userId);
        }
    }

    public string UserName
    {
        get
        {
            string userName = claimsPrincipal.FindFirstValue(CustomClaimsType.UserName);
            return userName is null ?
                throw new ClaimNotFoundException(CustomClaimsType.UserName) :
                userName;
        }
    }
}
