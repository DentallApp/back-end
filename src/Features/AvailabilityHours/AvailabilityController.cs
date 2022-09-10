namespace DentallApp.Features.AvailabilityHours;

[Route("availability")]
[ApiController]
public class AvailabilityController : ControllerBase
{
	private readonly IAvailabilityService _availabilityService;

	public AvailabilityController(IAvailabilityService availabilityService)
	{
		_availabilityService = availabilityService;
	}

	[AuthorizeByRole(RolesName.Secretary)]
	[Route("available-hours")]
	[HttpPost]
	public async Task<ActionResult<Response<IEnumerable<AvailableTimeRangeDto>>>> Get([FromBody]AvailableTimeRangePostDto availableTimeRangePostDto)
	{
		var response = await _availabilityService.GetAvailableHoursAsync(availableTimeRangePostDto);
		if (response.Success)
			return Ok(response);

		return BadRequest(response);
	}
}
