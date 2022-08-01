namespace DentallApp.Features.Dependents;

[AuthorizeByRole(RolesName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController : ControllerBase
{
    private readonly IDependentService _dependentService;
        
    public DependentController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [HttpPost]
    public async Task<ActionResult<Response>> Post([FromBody]DependentInsertDto dependentDto)
    {
        var response = await _dependentService.CreateDependentAsync(User.GetUserId(), dependentDto);
        if (response.Success)
            return CreatedAtAction(nameof(Post), response);

        return BadRequest(response);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<DependentGetDto>> GetByUserId()
        => await _dependentService.GetDependentsByUserIdAsync(User.GetUserId());

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(int id)
    {
        var response = await _dependentService.RemoveDependentAsync(id, User.GetUserId());
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Put(int id, [FromBody]DependentUpdateDto dependentDto)
    {
        var response = await _dependentService.UpdateDependentAsync(id, User.GetUserId(), dependentDto);
        if (response.Success)
            return Ok(response);

        return BadRequest(response);
    }
}
