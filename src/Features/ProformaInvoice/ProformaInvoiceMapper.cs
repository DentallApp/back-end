namespace DentallApp.Features.ProformaInvoice;

public static class ProformaInvoiceMapper
{
    public static object MapToObject(this ProformaInvoiceRequest request)
    {
        return new
        {
            request.FullName,
            request.Document,
            request.DateIssue,
            request.TotalPrice,
            request.DentalTreatments
        };
    }
}
