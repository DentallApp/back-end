using DentallApp.Core.Security.Employees.UseCases;

namespace DentallApp.Core.Security.Employees;

[Route("employee")]
[ApiController]
public class EmployeeController
{
    /// <summary>
    /// Creates a new employee account.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status409Conflict)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPost]
    public async Task<Result<CreatedId>> Create(
        [FromBody]CreateEmployeeRequest request,
        CreateEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Deletes the account of an employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpDelete("{id}")]
    public async Task<Result> Delete(
        int id,
        DeleteEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(id);

    /// <summary>
    /// Updates the information of the currently logged in employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Dentist, RoleName.Admin, RoleName.Superadmin)]
    [HttpPut]
    public async Task<Result> UpdateCurrentEmployee(
        [FromBody]UpdateCurrentEmployeeRequest request,
        UpdateCurrentEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// Updates an employee by ID.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<Result>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpPut("{id}")]
    public async Task<Result> UpdateAnyEmployee(
        int id, 
        [FromBody]UpdateAnyEmployeeRequest request,
        UpdateAnyEmployeeUseCase useCase)
        => await useCase.ExecuteAsync(id, request);

    /// <summary>
    /// Gets the employees that will be used to edit from the form.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AuthorizeByRole(RoleName.Admin, RoleName.Superadmin)]
    [HttpGet("edit")]
    public async Task<IEnumerable<GetEmployeesToEditResponse>> GetEmployeesToEdit(
        GetEmployeesToEditUseCase useCase)
        => await useCase.ExecuteAsync();

    /// <summary>
    /// Obtains dentists from a dental office.
    /// </summary>
    /// <remarks>
    /// Details to consider:
    /// <para>
    /// - If <c>OfficeId</c> is <c>0</c>, will obtain dentists from all offices.
    /// </para>
    /// <para>
    /// - If <c>IsDentistDeleted</c> is <c>true</c>, will obtain the dentists who have been temporarily deleted.
    /// </para>
    /// <para>
    /// - If <c>IsDentistDeleted</c> is <c>false</c>, will obtain the dentists who have not been temporarily deleted.
    /// </para>
    /// <para>
    /// - If <c>IsDentistDeleted</c> is <c>null</c>, will obtain all dentists, regardless of whether you are temporarily deleted or not.
    /// </para>
    /// </remarks>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<Result>(StatusCodes.Status403Forbidden)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Admin, RoleName.Superadmin)]
    [HttpPost("dentist")]
    public async Task<ListedResult<GetDentistsResponse>> GetDentists(
        [FromBody]GetDentistsRequest request,
        GetDentistsUseCase useCase)
        => await useCase.ExecuteAsync(request);

    /// <summary>
    /// You will get an overview of the information of each active and inactive employee.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AuthorizeByRole(RoleName.Secretary, RoleName.Admin)]
    [HttpGet("overview")]
    public async Task<IEnumerable<GetEmployeeOverviewResponse>> GetOverview(
        GetEmployeeOverviewUseCase useCase)
        => await useCase.ExecuteAsync();
}
