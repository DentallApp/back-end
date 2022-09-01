namespace DentallApp.Features.Offices.DTOs;

public class OfficeUpdateDto : OfficeInsertDto, ISoftDeleteDto
{
    public bool IsDeleted { get; set; }
    public bool DisableEmployeeAccounts { get; set; }
    [JsonIgnore]
    public bool EnableEmployeeAccounts => !DisableEmployeeAccounts;
}
