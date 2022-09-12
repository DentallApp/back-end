namespace DentallApp.Features.Appoinments.DTOs.CancelDTOs;

public class AppoinmentCancelByEmployeeDto
{
    public string OfficeName { get; set; }
    public string Reason { get; set; }
    public IEnumerable<AppoinmentDetailsByEmployeeDto> Appoinments { get; set; }
}
