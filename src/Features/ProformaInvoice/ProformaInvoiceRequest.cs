namespace DentallApp.Features.ProformaInvoice;

public class ProformaInvoiceRequest
{
    public class DentalTreatment
    {
        public string GeneralTreatmentName { get; init; }
        public string SpecificTreatmentName { get; init; }
        public double Price { get; init; }
    }

    public string FullName { get; init; }
    public string Document { get; init; }
    public DateTime DateIssue { get; init; }
    public double TotalPrice { get; init; }
    public IEnumerable<DentalTreatment> DentalTreatments { get; init; }
}
