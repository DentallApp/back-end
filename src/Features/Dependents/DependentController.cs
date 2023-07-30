namespace DentallApp.Features.Dependents;

[AuthorizeByRole(RolesName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateDependentRequest request,
        [FromServices]CreateDependentHandler handler)
    {
        var response = await handler.HandleAsync(User.GetUserId(), request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeleteDependentHandler handler)
    {
        var response = await handler.HandleAsync(id, User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id,
        [FromBody]UpdateDependentRequest request,
        [FromServices]UpdateDependentHandler handler)
    {
        var response = await handler.HandleAsync(id, User.GetUserId(), request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<GetDependentsByUserIdResponse>> GetByUserId(
        [FromServices]GetDependentsByUserIdHandler handler)
    {
        return await handler.HandleAsync(User.GetUserId());
    }
}
