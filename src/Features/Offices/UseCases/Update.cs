using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Features.Offices.UseCases;

public class UpdateOfficeRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }

    /// <summary>
    /// Un valor que indica sí el consultorio debe ser eliminado temporalmente.
    /// </summary>
    public bool IsDeleted { get; init; }

    /// <summary>
    /// Un valor que indica sí la casilla de verificación (checkbox) está marcada.
    /// Con esta propiedad se puede verificar sí las cuentas de los empleados deben ser eliminadas temporalmente cuando el consultorio queda inactivo.
    /// </summary>
    public bool IsCheckboxTicked { get; init; }

    /// <summary>
    /// Un valor que indica sí la casilla de verificación (checkbox) está desmarcada.
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

public class UpdateOfficeUseCase(DbContext context, IDateTimeService dateTimeService)
{
    public async Task<Result> ExecuteAsync(int officeId, int currentEmployeeId, UpdateOfficeRequest request)
    {
        var currentOffice = await context.Set<Office>()
            .Where(office => office.Id == officeId)
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
                    await DisableEmployeeAccountsAsync(currentEmployeeId, currentOffice);
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
