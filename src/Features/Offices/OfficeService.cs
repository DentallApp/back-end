namespace DentallApp.Features.Offices;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _officeRepository;

    public OfficeService(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<IEnumerable<OfficeGetDto>> GetOfficesAsync()
        => await _officeRepository.GetOfficesAsync();
}
