﻿namespace DentallApp.Features.FavoriteDentists.UseCases;

public class GetFavoriteAndUnfavoriteDentistsResponse
{
    public int DentistId { get; init; }
    public string FullName { get; init; }
    public string PregradeUniversity { get; init; }
    public string PostgradeUniversity { get; init; }
    public int OfficeId { get; init; }
    public string OfficeName { get; init; }
    /// <summary>
    /// Allows to verify if the dentist is preferred by the basic user.
    /// </summary>
    public bool IsFavorite { get; init; }
}

/// <summary>
/// Represents a use case to get the favorite and unfavorite dentists of a basic user.
/// </summary>
public class GetFavoriteAndUnfavoriteDentistsUseCase
{
    private readonly AppDbContext _context;

    public GetFavoriteAndUnfavoriteDentistsUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetFavoriteAndUnfavoriteDentistsResponse>> Execute(int userId)
    {
        var dentists = await 
            (from dentist in _context.Set<Employee>()
             join dentistDetails in _context.Set<Person>() on dentist.PersonId equals dentistDetails.Id
             join office in _context.Set<Office>() on dentist.OfficeId equals office.Id
             join userRole in _context.Set<UserRole>() on
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
             join favoriteDentist in _context.Set<FavoriteDentist>() on
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
             select new GetFavoriteAndUnfavoriteDentistsResponse
             {
                 DentistId           = dentist.Id,
                 FullName            = dentistDetails.FullName,
                 PregradeUniversity  = dentist.PregradeUniversity,
                 PostgradeUniversity = dentist.PostgradeUniversity,
                 OfficeId            = dentist.OfficeId,
                 OfficeName          = office.Name,
                 IsFavorite          = favoriteDentist != null
             })
             .AsNoTracking()
             .ToListAsync();

        return dentists;
    }
}
