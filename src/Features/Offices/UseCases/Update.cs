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
}

public static class UpdateOfficeMapper
{
    public static void MapToOffice(this UpdateOfficeRequest request, Office office)
    {
        office.Name             = request.Name;
        office.Address          = request.Address;
        office.ContactNumber    = request.ContactNumber;
        office.IsDeleted        = request.IsDeleted;
        office.IsCheckboxTicked = request.IsCheckboxTicked;
    }
}

public class UpdateOfficeUseCase
{
    private readonly AppDbContext _context;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdateOfficeUseCase(AppDbContext context, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response> Execute(int officeId, int currentEmployeeId, UpdateOfficeRequest request)
    {
        var currentOffice = await _context.Set<Office>()
            .Where(office => office.Id == officeId)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsyncEF();

        if (currentOffice is null)
            return new Response(ResourceNotFoundMessage);

        var strategy = _context.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            async () =>
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                if (request.IsInactive() && request.IsCheckboxTicked && currentOffice.IsEnabledEmployeeAccounts)
                    await DisableEmployeeAccountsAsync(currentEmployeeId, currentOffice);
                else if (request.IsInactive() && request.IsCheckboxUnticked && currentOffice.IsDisabledEmployeeAccounts)
                    await EnableEmployeeAccountsAsync(currentOffice);

                else if (request.IsActive() && currentOffice.IsDisabledEmployeeAccounts)
                    await EnableEmployeeAccountsAsync(currentOffice);

                request.MapToOffice(currentOffice);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            });

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    private async Task<int> DisableEmployeeAccountsAsync(int currentEmployeeId, Office office)
    {
        var affectedRows = await _context.Set<Employee>()
            .Where(employee => employee.Id != currentEmployeeId && employee.OfficeId == office.Id)
            .Set(employee => employee.IsDeleted, true)
            .Set(employee => employee.UpdatedAt, _dateTimeProvider.Now)
            .Set(employee => employee.User.UpdatedAt, _dateTimeProvider.Now)
            .Set(employee => employee.User.RefreshToken, e => null)
            .Set(employee => employee.User.RefreshTokenExpiry, e => null)
            .UpdateAsync();

        office.IsEnabledEmployeeAccounts = false;
        return affectedRows;
    }

    private async Task<int> EnableEmployeeAccountsAsync(Office office)
    {
        var affectedRows = await _context.Set<Employee>()
            .Where(employee => employee.OfficeId == office.Id)
            .IgnoreQueryFilters()
            .Set(employee => employee.IsDeleted, false)
            .Set(employee => employee.UpdatedAt, _dateTimeProvider.Now)
            .UpdateAsync();

        office.IsEnabledEmployeeAccounts = true;
        return affectedRows;
    }
}
