namespace DentallApp.Core.FavoriteDentists.UseCases;

public class GetFavoriteDentistsByUserIdResponse
{
    public int FavoriteDentistId { get; init; }
    public string FullName { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }
    public int OfficeId { get; init; }
    public string OfficeName { get; init; }
}

public class GetFavoriteDentistsByUserIdUseCase(DbContext context)
{
    public async Task<IEnumerable<GetFavoriteDentistsByUserIdResponse>> ExecuteAsync(int userId)
    {
        var favoriteDentists = await 
            (from favoriteDentist in context.Set<FavoriteDentist>()
             join dentist in context.Set<Employee>() on favoriteDentist.DentistId equals dentist.Id
             join dentistDetails in context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
             join office in context.Set<Office>() on dentist.OfficeId equals office.Id
             where favoriteDentist.UserId == userId
             select new GetFavoriteDentistsByUserIdResponse
             {
                 FavoriteDentistId   = favoriteDentist.Id,
                 FullName            = dentistDetails.FullName,
                 PregradeUniversity  = dentist.PregradeUniversity,
                 PostgradeUniversity = dentist.PostgradeUniversity,
                 OfficeId            = dentist.OfficeId,
                 OfficeName          = office.Name
             })
             .AsNoTracking()
             .ToListAsync();

        return favoriteDentists;
    }
}
