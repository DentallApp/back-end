namespace DentallApp.Infrastructure.Persistence;

/// <inheritdoc cref="ISchedulingQueries" />
public class SchedulingQueries : ISchedulingQueries
{
    private readonly AppDbContext _context;

    public SchedulingQueries(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
    {
        var treatments = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.SpecificTreatments.Any())
            .Select(treatment => new AdaptiveChoice
            {
                Title = treatment.Name,
                Value = treatment.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();

        return treatments;
    }

    public async Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId)
    {
        var dentists = await 
            (from employee in _context.Set<Employee>()
             join person in _context.Set<Person>() on employee.PersonId equals person.Id
             join office in _context.Set<Office>() on employee.OfficeId equals office.Id
             join employeeSpecialty in _context.Set<EmployeeSpecialty>()
              on employee.Id equals employeeSpecialty.EmployeeId into employeeSpecialties
             from employeeSpecialty in employeeSpecialties.DefaultIfEmpty()
             join userRole in _context.Set<UserRole>() on employee.UserId equals userRole.UserId
             where employee.IsActive() &&
                   employee.OfficeId == officeId &&
                   userRole.RoleId == RolesId.Dentist &&
                   employee.EmployeeSchedules.Any() &&
                   (employeeSpecialty.SpecialtyId == specialtyId || HasNoSpecialties(employee))
             select new AdaptiveChoice
             {
                 Title = person.FullName,
                 Value = employee.Id.ToString()
             })
             .IgnoreQueryFilters()
             .AsNoTracking()
             .ToListAsync();

        return dentists;
    }

    [Decompile]
    private static bool HasNoSpecialties(Employee employee)
    {
        return !employee.EmployeeSpecialties.Any();
    }

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
    {
        var offices = await _context.Set<Office>()
            .Where(office => office.OfficeSchedules.Any())
            .Select(office => new AdaptiveChoice
            {
                Title = office.Name,
                Value = office.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();

        return offices;
    }

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(AuthenticatedUser user)
    { 
        var choices = await _context.Set<Dependent>()
            .Where(dependent => dependent.UserId == user.UserId)
            .Select(dependent => new AdaptiveChoice
            {
                Title = dependent.Person.FullName + " / " + dependent.Kinship.Name,
                Value = dependent.Person.Id.ToString()
            })
            .AsNoTracking()
            .ToListAsync();
        
        choices.Insert(0, new AdaptiveChoice
        {
            Title = user.FullName + " / " + KinshipsName.Default,
            Value = user.PersonId.ToString()
        });

        return choices;
    }
}
