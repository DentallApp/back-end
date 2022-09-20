namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<IEnumerable<ReportGetAppoinmentDto>> GetAppoinmentsByDateRangeAsync(ReportPostWithStatusDto reportPostDto);
}
