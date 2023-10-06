namespace DentallApp.Shared.Models.Claims;

public class EmployeeClaims : UserClaims
{
    public int EmployeeId { get; init; }
    public int OfficeId { get; init; }
}
