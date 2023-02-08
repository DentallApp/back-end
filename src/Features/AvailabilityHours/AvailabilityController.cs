namespace DentallApp.Features.AvailabilityHours;

[Route("availability")]
[ApiController]
public class AvailabilityController : ControllerBase
{
	private readonly AvailabilityService _availabilityService;

	public AvailabilityController(AvailabilityService availabilityService)
	{
		_availabilityService = availabilityService;
	}

	[AuthorizeByRole(RolesName.Secretary)]
	[Route("available-hours")]
	[HttpPost]
	public async Task<ActionResult<Response<IEnumerable<AvailableTimeRangeDto>>>> Get([FromBody]AvailableTimeRangePostDto availableTimeRangePostDto)
	{
		var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);
		return response.Success ? Ok(response) : BadRequest(response);
	}
}
