namespace DentallApp.Features.Offices;

public interface IOfficeService
{
    Task<Response> CreateOfficeAsync(OfficeInsertDto officeInsertDto);
    Task<Response> UpdateOfficeAsync(int officeId, int currentEmployeeId, OfficeUpdateDto officeUpdateDto);
}
