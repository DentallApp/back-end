namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<ReportGetTotalAppointmentDto> GetTotalAppointmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetTotalScheduledAppointmentDto>> GetTotalScheduledAppointmentsByDateRangeAsync(ReportPostDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
