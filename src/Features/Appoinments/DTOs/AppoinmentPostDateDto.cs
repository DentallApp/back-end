namespace DentallApp.Features.Appoinments.DTOs;

public class AppoinmentPostDateDto
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public class AppoinmentPostDateWithDentistDto : AppoinmentPostDateDto
{
    public int DentistId { get; set; }
}
