namespace DentallApp.Features.AppoinmentsStatus;

public class AppoinmentStatusRepository : Repository<AppoinmentStatus>, IAppoinmentStatusRepository
{
    public AppoinmentStatusRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<AppoinmentStatusGetDto>> GetAllStatusAsync()
        => await Context.Set<AppoinmentStatus>()
                        .Select(appoinmentStatus => appoinmentStatus.MapToAppoinmentStatusGetDto())
                        .ToListAsync();
}
