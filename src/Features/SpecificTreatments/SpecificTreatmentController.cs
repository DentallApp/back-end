namespace DentallApp.Features.SpecificTreatments;

[Route("specific-treatment")]
[ApiController]
public class SpecificTreatmentController : ControllerBase
{
    private readonly ISpecificTreatmentService _service;

    public SpecificTreatmentController(ISpecificTreatmentService service)
    {
        _service = service;
    }

    [AuthorizeByRole(RolesName.BasicUser, RolesName.Superadmin)]
    [HttpGet("{generalTreatmentId}")]
    public async Task<IEnumerable<SpecificTreatmentGetDto>> Get(int generalTreatmentId)
        => await _service.GetSpecificTreatmentsByGeneralTreatmentIdAsync(generalTreatmentId);
}
