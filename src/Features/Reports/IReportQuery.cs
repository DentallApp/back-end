namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<ReportGetTotalAppoinmentDto> GetTotalAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetTotalScheduledAppoinmentDto>> GetTotalScheduledAppoinmentsByDateRangeAsync(ReportPostDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
