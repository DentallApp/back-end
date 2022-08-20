namespace DentallApp.Features.ProformaInvoice;

public static class ProformaInvoiceMapper
{
    public static object MapToObject(this ProformaInvoiceDto proformaInvoiceDto)
    {
        var model = new
        {
            proformaInvoiceDto.FullName,
            proformaInvoiceDto.Document,
            proformaInvoiceDto.DateIssue,
            proformaInvoiceDto.TotalPrice,
            proformaInvoiceDto.DentalTreatments
        };
        return model;
    }
}
