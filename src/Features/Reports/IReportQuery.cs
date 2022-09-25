namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<ReportGetTotalAppoinmentDto> GetTotalAppoinmentsByDateRangeAsync(ReportPostDto reportPostDto);
    Task<IEnumerable<ReportGetScheduledAppoinmentDto>> GetScheduledAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
