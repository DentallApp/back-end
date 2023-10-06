namespace DentallApp.Shared.Models.Claims;

public class UserClaims
{
    public int UserId { get; init; }
    public int PersonId { get; init; }
    public string UserName { get; init; }
    public string FullName { get; init; }
    public IEnumerable<string> Roles { get; init; }
}
