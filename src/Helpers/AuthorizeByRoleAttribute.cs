namespace DentallApp.Helpers;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(params string[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
