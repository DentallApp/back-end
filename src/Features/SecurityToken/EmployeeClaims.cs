namespace DentallApp.Features.SecurityToken;

public class EmployeeClaims : UserClaims
{
    public int EmployeeId { get; set; }
    public int OfficeId { get; set; }
}
