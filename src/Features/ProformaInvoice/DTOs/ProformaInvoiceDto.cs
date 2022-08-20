namespace DentallApp.Features.ProformaInvoice.DTOs;

public class ProformaInvoiceDto
{
    public string FullName { get; set; }
    public string Document { get; set; }
    public DateTime DateIssue { get; set; }
    public double TotalPrice { get; set; }
    public IEnumerable<DentalTreatmentDto> DentalTreatments { get; set; }
}
