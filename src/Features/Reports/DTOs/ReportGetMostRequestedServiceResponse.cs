namespace DentallApp.Features.Reports.DTOs;

public class ReportGetMostRequestedServiceResponse
{
    public string DentalServiceName { get; set; }
    public int TotalAppointmentsAssisted { get; set; }
}
