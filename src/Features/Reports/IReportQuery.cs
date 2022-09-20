namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<IEnumerable<ReportGetAppoinmentDto>> GetAppoinmentsByDateRangeAsync(ReportPostWithStatusDto reportPostDto);
    Task<IEnumerable<ReportGetScheduledAppoinmentDto>> GetScheduledAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
