namespace DentallApp.Features.Offices;

public interface IOfficeRepository
{
    Task<IEnumerable<OfficeGetDto>> GetOfficesAsync();
}
