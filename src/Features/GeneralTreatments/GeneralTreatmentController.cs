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
        => await _service.GetTreatmentsAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GeneralTreatmentGetDto>>> Get(int id)
    {
        var response = await _service.GetTreatmentByIdAsync(id);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromForm]GeneralTreatmentInsertDto treatmentInsertDto)
    {
        var response = await _service.CreateTreatmentAsync(treatmentInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }
}
