namespace DentallApp.Shared.Persistence;

/// <summary>
/// Represents queries for appointment scheduling.
/// </summary>
public interface ISchedulingQueries
{
    Task<List<SchedulingResponse>> GetPatientsAsync(AuthenticatedUser user);
    Task<List<SchedulingResponse>> GetOfficesAsync();
    Task<List<SchedulingResponse>> GetDentalServicesAsync();
    Task<List<SchedulingResponse>> GetDentistsAsync(int officeId, int specialtyId);
}
