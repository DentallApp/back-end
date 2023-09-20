namespace DentallApp.Shared.Persistence.Repositories;

public interface IEmployeeSpecialtyRepository : IRepository<EmployeeSpecialty>
{
    /// <summary>
    /// Adds, updates or removes the specialties to a employee.
    /// </summary>
    /// <param name="employeeId">The ID of the employee to update.</param>
    /// <param name="currentEmployeeSpecialties">A collection with the current specialties of a employee.</param>
    /// <param name="specialtiesId">A collection of specialty identifiers obtained from a web client.</param>
    void UpdateEmployeeSpecialties(int employeeId, List<EmployeeSpecialty> currentEmployeeSpecialties, List<int> specialtiesId);
}
