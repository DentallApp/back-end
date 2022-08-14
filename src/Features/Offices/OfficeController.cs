namespace DentallApp.Features.Offices;

[Route("office")]
[ApiController]
public class OfficeController : ControllerBase
{
    private readonly IOfficeService _officeService;

    public OfficeController(IOfficeService officeService)
    {
        _officeService = officeService;
    }

    [HttpGet]
    public async Task<IEnumerable<OfficeGetDto>> Get()
        => await _officeService.GetOfficesAsync();
        
}
