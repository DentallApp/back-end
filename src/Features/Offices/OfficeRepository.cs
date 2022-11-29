using LinqToDB;
using LinqToDB.EntityFrameworkCore;
namespace DentallApp.Features.Offices;

public class OfficeRepository : SoftDeleteRepository<Office>, IOfficeRepository
{
    public OfficeRepository(AppDbContext context) : base(context) { }

    public async Task<Office> GetOfficeByIdAsync(int id)
        => await Context.Set<Office>()
                        .Where(office => office.Id == id)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsyncEF();

    public async Task<IEnumerable<OfficeGetDto>> GetOfficesAsync()
        => await Context.Set<Office>()
                        .Select(office => office.MapToOfficeGetDto())
                        .ToListAsyncEF();

    public async Task<IEnumerable<OfficeGetDto>> GetAllOfficesAsync()
        => await Context.Set<Office>()
                        .Select(office => office.MapToOfficeGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsyncEF();

    public async Task<IEnumerable<OfficeShowDto>> GetOfficesForEditAsync()
        => await Context.Set<Office>()
                        .Select(office => office.MapToOfficeShowDto())
                        .IgnoreQueryFilters()
                        .ToListAsyncEF();

    public async Task<int> DisableEmployeeAccountsAsync(int currentEmployeeId, Office office)
    {
        var affectedRows = await Context.Set<Employee>()
                                        .Where(employee => employee.Id != currentEmployeeId && employee.OfficeId == office.Id)
                                        .Set(employee => employee.IsDeleted, true)
                                        .Set(employee => employee.UpdatedAt, DateTime.Now)
                                        .Set(employee => employee.User.UpdatedAt, DateTime.Now)
                                        .Set(employee => employee.User.RefreshToken, e => null)
                                        .Set(employee => employee.User.RefreshTokenExpiry, e => null)
                                        .UpdateAsync();
        office.IsEnabledEmployeeAccounts = false;
        return affectedRows;
    }

    public async Task<int> EnableEmployeeAccountsAsync(Office office)
    {
        var affectedRows = await Context.Set<Employee>()
                                        .Where(employee => employee.OfficeId == office.Id)
                                        .IgnoreQueryFilters()
                                        .Set(employee => employee.IsDeleted, false)
                                        .Set(employee => employee.UpdatedAt, DateTime.Now)
                                        .UpdateAsync();
        office.IsEnabledEmployeeAccounts = true;
        return affectedRows;
    }

    public IExecutionStrategy CreateExecutionStrategy() => Context.Database.CreateExecutionStrategy();
}
