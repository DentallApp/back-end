using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Core.Offices.UseCases;

public class UpdateOfficeRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }

    /// <summary>
    /// A value that indicates whether the dental office should be temporarily deleted.
    /// </summary>
    public bool IsDeleted { get; init; }

    /// <summary>
    /// A value that indicates whether the checkbox is checked. 
    /// </summary>
    /// <remarks>
    /// With this property you can check whether employee accounts 
    /// should be temporarily deleted when the dental office becomes inactive.
    /// </remarks>
    public bool IsCheckboxTicked { get; init; }

    /// <summary>
    /// A value that indicates whether the checkbox is unchecked.
    /// </summary>
    [JsonIgnore]
    public bool IsCheckboxUnticked => !IsCheckboxTicked;

    public bool IsActive() => !IsDeleted;
    public bool IsInactive() => IsDeleted;

    public void MapToOffice(Office office)
    {
        office.Name             = Name;
        office.Address          = Address;
        office.ContactNumber    = ContactNumber;
        office.IsDeleted        = IsDeleted;
        office.IsCheckboxTicked = IsCheckboxTicked;
    }
}

public class UpdateOfficeValidator : AbstractValidator<UpdateOfficeRequest>
{
    public UpdateOfficeValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
    }
}

public class UpdateOfficeUseCase(
    DbContext context, 
    IDateTimeService dateTimeService,
    ICurrentEmployee currentEmployee,
    UpdateOfficeValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdateOfficeRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var currentOffice = await context.Set<Office>()
            .Where(office => office.Id == id)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsyncEF();

        if (currentOffice is null)
            return Result.NotFound();

        var strategy = context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync();
                if (request.IsInactive() && request.IsCheckboxTicked && currentOffice.IsEnabledEmployeeAccounts)
                    await DisableEmployeeAccountsAsync(currentEmployee.EmployeeId, currentOffice);
                else if (request.IsInactive() && request.IsCheckboxUnticked && currentOffice.IsDisabledEmployeeAccounts)
                    await EnableEmployeeAccountsAsync(currentOffice);

                else if (request.IsActive() && currentOffice.IsDisabledEmployeeAccounts)
                    await EnableEmployeeAccountsAsync(currentOffice);

                request.MapToOffice(currentOffice);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            });

        return Result.UpdatedResource();
    }

    private async Task<int> DisableEmployeeAccountsAsync(int currentEmployeeId, Office office)
    {
        var affectedRows = await context.Set<Employee>()
            .Where(employee => employee.Id != currentEmployeeId && employee.OfficeId == office.Id)
            .Set(employee => employee.IsDeleted, true)
            .Set(employee => employee.UpdatedAt, dateTimeService.Now)
            .Set(employee => employee.User.UpdatedAt, dateTimeService.Now)
            .Set(employee => employee.User.RefreshToken, e => null)
            .Set(employee => employee.User.RefreshTokenExpiry, e => null)
            .UpdateAsync();

        office.IsEnabledEmployeeAccounts = false;
        return affectedRows;
    }

    private async Task<int> EnableEmployeeAccountsAsync(Office office)
    {
        var affectedRows = await context.Set<Employee>()
            .Where(employee => employee.OfficeId == office.Id)
            .IgnoreQueryFilters()
            .Set(employee => employee.IsDeleted, false)
            .Set(employee => employee.UpdatedAt, dateTimeService.Now)
            .UpdateAsync();

        office.IsEnabledEmployeeAccounts = true;
        return affectedRows;
    }
}
