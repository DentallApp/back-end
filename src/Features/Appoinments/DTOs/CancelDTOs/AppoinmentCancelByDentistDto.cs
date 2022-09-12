namespace DentallApp.Features.Appoinments.DTOs.CancelDTOs;

public class AppoinmentCancelByDentistDto
{
    public string DentistName { get; set; }
    public string OfficeName { get; set; }
    public string Reason { get; set; }
    public IEnumerable<AppoinmentDetailsByDentistDto> Appoinments { get; set; }
}
