namespace DentallApp.Features.FavoriteDentists;

public class FavoriteDentistRepository : Repository<FavoriteDentist>, IFavoriteDentistRepository
{
    public FavoriteDentistRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<FavoriteDentistGetDto>> GetFavoriteDentistsAsync(int userId)
        => await (from favoriteDentist in Context.Set<FavoriteDentist>()
                  join dentist in Context.Set<Employee>() on favoriteDentist.DentistId equals dentist.Id
                  join dentistDetails in Context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
                  join office in Context.Set<Office>() on dentist.OfficeId equals office.Id
                  where favoriteDentist.UserId == userId
                  select new FavoriteDentistGetDto
                  {
                      FavoriteDentistId     = favoriteDentist.Id,
                      FullName              = dentistDetails.FullName,
                      PregradeUniversity    = dentist.PregradeUniversity,
                      PostgradeUniversity   = dentist.PostgradeUniversity,
                      OfficeId              = dentist.OfficeId,
                      OfficeName            = office.Name
                  }).ToListAsync();

    public async Task<IEnumerable<DentistGetDto>> GetListOfDentistsAsync(int userId)
        => await (from dentist in Context.Set<Employee>()
                  join dentistDetails in Context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
                  join office in Context.Set<Office>() on dentist.OfficeId equals office.Id
                  join userRole in Context.Set<UserRole>() on 
                      new 
                      { 
                          RoleId = RolesId.Dentist, 
                          dentist.UserId 
                      } 
                      equals
                      new 
                      { 
                          userRole.RoleId,          
                          userRole.UserId 
                      }
                  join favoriteDentist in Context.Set<FavoriteDentist>() on
                      new 
                      { 
                          UserId = userId,
                          dentist.Id
                      } 
                      equals 
                      new 
                      { 
                          favoriteDentist.UserId, 
                          Id = favoriteDentist.DentistId 
                      } into favoriteDentists
                  from favoriteDentist in favoriteDentists.DefaultIfEmpty()
                  select new DentistGetDto
                  {
                      DentistId             = dentist.Id,
                      FullName              = dentistDetails.FullName,
                      PregradeUniversity    = dentist.PregradeUniversity,
                      PostgradeUniversity   = dentist.PostgradeUniversity,
                      OfficeId              = dentist.OfficeId,
                      OfficeName            = office.Name,
                      IsFavorite            = favoriteDentist != null
                  }).ToListAsync();

    public async Task<FavoriteDentist> GetByUserIdAndDentistIdAsync(int userId, int dentistId)
        => await Context.Set<FavoriteDentist>()
                        .Where(favoriteDentist => 
                                favoriteDentist.UserId == userId && 
                                favoriteDentist.DentistId == dentistId
                              )
                        .FirstOrDefaultAsync();

}