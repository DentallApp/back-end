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

    [HttpGet("name")]
    public async Task<IEnumerable<GeneralTreatmentGetNameDto>> GetTreatmentsWithName()
        => await _service.GetTreatmentsWithNameAsync();

    [HttpGet("edit")]
    public async Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsForEdit()
        => await _service.GetTreatmentsWithoutImageUrlAsync();

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

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromForm]GeneralTreatmentUpdateDto treatmentUpdateDto)
    {
        var response = await _service.UpdateTreatmentAsync(id, treatmentUpdateDto);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _service.RemoveTreatmentAsync(id);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }
}
