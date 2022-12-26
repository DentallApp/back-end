namespace DentallApp.Features.Chatbot;

public class BotQueryRepository : IBotQueryRepository
{
    private readonly AppDbContext _context;

    public BotQueryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AdaptiveChoice>> GetDentalServicesAsync()
        => await _context.Set<GeneralTreatment>()
                         .Where(treatment => treatment.SpecificTreatments.Any())
                         .Select(treatment => new AdaptiveChoice
                          {
                             Title = treatment.Name,
                             Value = treatment.Id.ToString()
                          })
                         .ToListAsync();

    public async Task<List<AdaptiveChoice>> GetDentistsAsync(int officeId, int specialtyId)
        => await (from employee in _context.Set<Employee>()
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
                       (employeeSpecialty.SpecialtyId == specialtyId ||
                        HasNoSpecialties(employee))
                  select new AdaptiveChoice 
                  { 
                      Title = person.FullName, 
                      Value = employee.Id.ToString() 
                  })
                .IgnoreQueryFilters()
                .ToListAsync();

    [Decompile]
    private bool HasNoSpecialties(Employee employee) => !employee.EmployeeSpecialties.Any();

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
        => await _context.Set<Office>()
                         .Where(office => office.OfficeSchedules.Any())
                         .Select(office => new AdaptiveChoice
                         {
                             Title = office.Name,
                             Value = office.Id.ToString()
                         })
                         .ToListAsync();

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile)
    { 
        var choices = await _context.Set<Dependent>()
                                    .Include(dependent => dependent.Kinship)
                                    .Include(dependent => dependent.Person)
                                    .Where(dependent => dependent.UserId == userProfile.Id)
                                    .Select(dependent => new AdaptiveChoice
                                    {
                                        Title = dependent.Person.FullName + " / " + dependent.Kinship.Name,
                                        Value = dependent.Person.Id.ToString()
                                    })
                                    .ToListAsync();
        
        choices.Insert(0, new AdaptiveChoice
        {
            Title = userProfile.FullName + " / " + KinshipsName.Default,
            Value = userProfile.PersonId.ToString()
        });
        return choices;
    }
}
