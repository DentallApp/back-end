namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController : ControllerBase
{
    private readonly ISpecificTreatmentService _treatmentService;
    private readonly ISpecificTreatmentRepository _treatmentRepository;

    public SpecificTreatmentController(ISpecificTreatmentService treatmentService, 
                                       ISpecificTreatmentRepository treatmentRepository)
    {
        _treatmentService = treatmentService;
        _treatmentRepository = treatmentRepository;
    }

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<SpecificTreatmentGetDto>> Get(int generalTreatmentId)
        => await _treatmentRepository.GetSpecificTreatmentsByGeneralTreatmentIdAsync(generalTreatmentId);

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet]
    public async Task<IEnumerable<SpecificTreatmentShowDto>> Get()
        => await _treatmentRepository.GetSpecificTreatmentsAsync();

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromBody]SpecificTreatmentInsertDto treatmentInsertDto)
    {
        var response = await _treatmentService.CreateSpecificTreatmentAsync(treatmentInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]SpecificTreatmentUpdateDto treatmentUpdateDto)
    {
        var response = await _treatmentService.UpdateSpecificTreatmentAsync(id, treatmentUpdateDto);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [AuthorizeByRole(RolesName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _treatmentService.RemoveSpecificTreatmentAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }
}
