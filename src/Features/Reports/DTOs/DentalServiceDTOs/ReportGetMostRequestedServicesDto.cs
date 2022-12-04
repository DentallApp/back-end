namespace DentallApp.Features.Reports.DTOs;

public class ReportGetMostRequestedServicesDto
{
    public string DentalServiceName { get; set; }
    public int TotalAppointmentsAssisted { get; set; }
}
