namespace DentallApp.Infrastructure.Persistence.Repositories;

public class EmployeeSpecialtyRepository : Repository<EmployeeSpecialty>, IEmployeeSpecialtyRepository
{
    public EmployeeSpecialtyRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc />
    public void UpdateEmployeeSpecialties(int employeeId, List<EmployeeSpecialty> currentEmployeeSpecialties, List<int> specialtiesId)
        => this.AddOrUpdateOrDelete(key: employeeId, source: ref currentEmployeeSpecialties, identifiers: ref specialtiesId);
}
