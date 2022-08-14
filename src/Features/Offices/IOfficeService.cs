namespace DentallApp.Features.Offices;

public interface IOfficeService
{
    Task<IEnumerable<OfficeGetDto>> GetOfficesAsync();
}
