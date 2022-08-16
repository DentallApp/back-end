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
                         .Select(treatment => new AdaptiveChoice
                         {
                             Title = treatment.Name,
                             Value = treatment.Id.ToString()
                         })
                         .ToListAsync();

    public async Task<List<AdaptiveChoice>> GetDentistsByOfficeIdAsync(int officeId)
        => await (from employee in _context.Set<Employee>()
                  join person in _context.Set<Person>() on employee.PersonId equals person.Id
                  join office in _context.Set<Office>() on employee.OfficeId equals office.Id
                  join userRole in _context.Set<UserRole>() on employee.UserId equals userRole.UserId
                  where employee.OfficeId == officeId && userRole.RoleId == RolesId.Dentist
                  select new AdaptiveChoice 
                  { 
                      Title = person.FullName, 
                      Value = employee.Id.ToString() 
                  }
                 ).ToListAsync();

    public async Task<List<AdaptiveChoice>> GetOfficesAsync()
        => await _context.Set<Office>()
                         .Select(office => new AdaptiveChoice
                         {
                             Title = office.Name,
                             Value = office.Id.ToString()
                         })
                         .ToListAsync();

    public async Task<List<AdaptiveChoice>> GetPatientsAsync(UserProfile userProfile)
    { 
        var choices = await _context.Set<Dependent>()
                                    .Include(dependent => dependent.Person)
                                    .Where(dependent => dependent.UserId == userProfile.Id)
                                    .Select(dependent => new AdaptiveChoice
                                    {
                                        Title = dependent.Person.FullName,
                                        Value = dependent.Person.Id.ToString()
                                    })
                                    .ToListAsync();
        
        choices.Add(new AdaptiveChoice
        {
            Title = userProfile.FullName,
            Value = userProfile.PersonId.ToString()
        });
        return choices;
    }
}
