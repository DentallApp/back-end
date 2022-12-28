namespace DentallApp.Features.GeneralTreatments;

[Route("general-treatment")]
[ApiController]
public class GeneralTreatmentController : ControllerBase
{
    private readonly IGeneralTreatmentService _treatmentService;
    private readonly IGeneralTreatmentRepository _treatmentRepository;

    public GeneralTreatmentController(IGeneralTreatmentService treatmentService, 
                                      IGeneralTreatmentRepository treatmentRepository)
    {
        _treatmentService = treatmentService;
        _treatmentRepository = treatmentRepository;
    }

    [HttpGet("name")]
    public async Task<IEnumerable<GeneralTreatmentGetNameDto>> GetTreatmentsWithName()
        => await _treatmentRepository.GetTreatmentsWithNameAsync();

    [HttpGet("edit")]
    public async Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsForEdit()
        => await _treatmentRepository.GetTreatmentsWithoutImageUrlAsync();

    [HttpGet]
    public async Task<IEnumerable<GeneralTreatmentGetDto>> Get()
        => await _treatmentRepository.GetTreatmentsAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GeneralTreatmentGetDto>>> Get(int id)
    {
        var response = await _treatmentService.GetTreatmentByIdAsync(id);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromForm]GeneralTreatmentInsertDto treatmentInsertDto)
    {
        var response = await _treatmentService.CreateTreatmentAsync(treatmentInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromForm]GeneralTreatmentUpdateDto treatmentUpdateDto)
    {
        var response = await _treatmentService.UpdateTreatmentAsync(id, treatmentUpdateDto);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _treatmentService.RemoveTreatmentAsync(id);
        if (response.Success)
            return Ok(response);

        return NotFound(response);
    }
}
