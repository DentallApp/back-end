namespace DentallApp.Features.Reports.DTOs;

public class ReportGetMostRequestedServicesResponse
{
    public string DentalServiceName { get; set; }
    public int TotalAppointmentsAssisted { get; set; }
}
