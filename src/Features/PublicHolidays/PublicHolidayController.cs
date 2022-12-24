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
    public async Task<ActionResult<Response>> Post([FromBody]PublicHolidayInsertDto holidayInsertDto)
    {
        var response = await _holidayService.CreatePublicHolidayAsync(holidayInsertDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _holidayService.RemovePublicHolidayAsync(id);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]PublicHolidayUpdateDto holidayUpdateDto)
    {
        var response = await _holidayService.UpdatePublicHolidayAsync(id, holidayUpdateDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpGet]
    public async Task<IEnumerable<PublicHolidayGetAllDto>> GetAll()
        => await _holidayRepository.GetPublicHolidaysAsync();
}
