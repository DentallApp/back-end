namespace DentallApp.Features.AppoinmentsStatus;

[Route("appoinment-status")]
[ApiController]
public class AppoinmentStatusController : ControllerBase
{
    private readonly IAppoinmentStatusRepository _repository;

    public AppoinmentStatusController(IAppoinmentStatusRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<AppoinmentStatusGetDto>> Get()
        => await _repository.GetAllStatusAsync();
}
