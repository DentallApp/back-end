namespace DentallApp.Features.AvailabilityHours;

[Route("availability")]
[ApiController]
public class AvailabilityController : ControllerBase
{
	[AuthorizeByRole(RolesName.Secretary)]
	[Route("available-hours")]
	[HttpPost]
	public async Task<ActionResult<Response<IEnumerable<AvailableTimeRangeResponse>>>> Get(
		[FromBody]AvailableTimeRangeRequest request,
		[FromServices]GetAvailableHoursUseCase useCase)
	{
		var response = await useCase.Execute(request);
		return response.Success ? Ok(response) : BadRequest(response);
	}
}
