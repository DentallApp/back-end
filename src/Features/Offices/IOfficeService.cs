namespace DentallApp.Features.Offices;

public interface IOfficeService
{
    Task<Response<DtoBase>> CreateOfficeAsync(OfficeInsertDto officeInsertDto);
    Task<Response> UpdateOfficeAsync(int officeId, int currentEmployeeId, OfficeUpdateDto officeUpdateDto);
}
