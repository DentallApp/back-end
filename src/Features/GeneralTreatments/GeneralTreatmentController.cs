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
        return response.Success ? Ok(response) :  NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromForm]GeneralTreatmentInsertDto treatmentInsertDto)
    {
        var response = await _treatmentService.CreateTreatmentAsync(treatmentInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromForm]GeneralTreatmentUpdateDto treatmentUpdateDto)
    {
        var response = await _treatmentService.UpdateTreatmentAsync(id, treatmentUpdateDto);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _treatmentService.RemoveTreatmentAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }
}
