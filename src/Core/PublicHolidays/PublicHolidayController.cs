using DentallApp.Core.PublicHolidays.UseCases;

namespace DentallApp.Core.PublicHolidays;

[AuthorizeByRole(RoleName.Superadmin)]
[Route("public-holiday")]
[ApiController]
public class PublicHolidayController
{
    /// <summary>
    /// Creates a new public holiday for a set of dental offices.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreatePublicHolidayRequest request,
        CreatePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Deletes a public holiday by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeletePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Updates a public holiday by its ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [HttpPut("{id}")]
    public async Task<Result> Update(
        int id, 
        [FromBody]UpdatePublicHolidayRequest request,
        UpdatePublicHolidayUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Obtains all holidays including dental offices.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IEnumerable<GetPublicHolidaysResponse>> GetAll(
        GetPublicHolidaysUseCase useCase)
        => await useCase.ExecuteAsync();
}
