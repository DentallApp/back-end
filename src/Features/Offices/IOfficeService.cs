namespace DentallApp.Features.Offices;

public interface IOfficeService
{
    Task<IEnumerable<OfficeGetDto>> GetOfficesAsync();
    Task<IEnumerable<OfficeGetDto>> GetAllOfficesAsync();
    Task<Response> CreateOfficeAsync(OfficeInsertDto officeInsertDto);
    Task<Response> UpdateOfficeAsync(int officeId, int currentEmployeeId, OfficeUpdateDto officeUpdateDto);
    Task<IEnumerable<OfficeShowDto>> GetOfficesForEditAsync();
}
