namespace DentallApp.Features.PublicHolidays;

[AuthorizeByRole(RolesName.Superadmin)]
[Route("public-holiday")]
[ApiController]
public class PublicHolidayController : ControllerBase
{
    private readonly IPublicHolidayService _holidayService;
    private readonly IPublicHolidayRepository _holidayRepository;

    public PublicHolidayController(IPublicHolidayService holidayService,
                                   IPublicHolidayRepository holidayRepository)
    {
        _holidayService = holidayService;
        _holidayRepository = holidayRepository;
    }

    [HttpPost]
    public async Task<ActionResult<Response<DtoBase>>> Post([FromBody]PublicHolidayInsertDto holidayInsertDto)
    {
        var response = await _holidayService.CreatePublicHolidayAsync(holidayInsertDto);
        return response.Success ? CreatedAtAction(nameof(Post), response) : BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _holidayService.RemovePublicHolidayAsync(id);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]PublicHolidayUpdateDto holidayUpdateDto)
    {
        var response = await _holidayService.UpdatePublicHolidayAsync(id, holidayUpdateDto);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    public async Task<IEnumerable<PublicHolidayGetAllDto>> GetAll()
        => await _holidayRepository.GetPublicHolidaysAsync();
}
