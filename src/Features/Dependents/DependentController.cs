using DentallApp.Features.Dependents.UseCases;

namespace DentallApp.Features.Dependents;

[AuthorizeByRole(RolesName.BasicUser)]
[Route("dependent")]
[ApiController]
public class DependentController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Response<InsertedIdDto>>> Create(
        [FromBody]CreateDependentRequest request,
        [FromServices]CreateDependentUseCase useCase)
    {
        var response = await useCase.Execute(User.GetUserId(), request);
        return response.Success ? CreatedAtAction(nameof(Create), response) : BadRequest(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Response>> Delete(
        int id,
        [FromServices]DeleteDependentUseCase useCase)
    {
        var response = await useCase.Execute(id, User.GetUserId());
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Response>> Update(
        int id,
        [FromBody]UpdateDependentRequest request,
        [FromServices]UpdateDependentUseCase useCase)
    {
        var response = await useCase.Execute(id, User.GetUserId(), request);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [Route("user")]
    [HttpGet]
    public async Task<IEnumerable<GetDependentsByUserIdResponse>> GetByUserId(
        [FromServices]GetDependentsByUserIdUseCase useCase)
    {
        return await useCase.Execute(User.GetUserId());
    }
}
