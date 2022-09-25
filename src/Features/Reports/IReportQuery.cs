namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<ReportGetTotalAppoinmentDto> GetTotalAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetScheduledAppoinmentDto>> GetScheduledAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
