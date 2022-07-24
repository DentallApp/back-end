namespace DentallApp.Features.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController : ControllerBase
{
    private readonly IGeneralTreatmentService _service;

    public GeneralTreatmentController(IGeneralTreatmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<GeneralTreatmentGetDto>> Get()
        => await _service.GetTreatments();

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GeneralTreatmentGetDto>>> Get(int id)
    {
        var response = await _service.GetTreatmentById(id);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }
}
