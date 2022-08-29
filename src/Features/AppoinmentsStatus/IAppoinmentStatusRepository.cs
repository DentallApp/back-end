namespace DentallApp.Features.AppoinmentsStatus;

public interface IAppoinmentStatusRepository : IRepository<AppoinmentStatus>
{
    Task<IEnumerable<AppoinmentStatusGetDto>> GetAllStatusAsync();
}
