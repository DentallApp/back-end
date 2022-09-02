namespace DentallApp.Features.Offices;

public interface IOfficeRepository : ISoftDeleteRepository<Office>
{
    Task<IEnumerable<OfficeGetDto>> GetOfficesAsync();
    Task<IEnumerable<OfficeGetDto>> GetAllOfficesAsync();
    Task<IEnumerable<OfficeShowDto>> GetOfficesForEditAsync();
    Task<Office> GetOfficeByIdAsync(int id);
    Task<int> EnableEmployeeAccountsAsync(Office office);
    Task<int> DisableEmployeeAccountsAsync(int currentEmployeeId, Office office);
}
