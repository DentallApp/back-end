namespace DentallApp.Features.Reports;

public interface IReportQuery
{
    Task<ReportGetTotalAppointmentResponse> GetTotalAppointmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto);
    Task<IEnumerable<ReportGetTotalScheduledAppointmentDto>> GetTotalScheduledAppointmentsByDateRangeAsync(ReportPostDto reportPostDto);
    Task<IEnumerable<ReportGetMostRequestedServicesResponse>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto);
}
