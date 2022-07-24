namespace DentallApp.Features.SecurityToken;

public class UserClaims
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
