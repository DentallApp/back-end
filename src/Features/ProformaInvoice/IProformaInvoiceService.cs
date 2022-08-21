namespace DentallApp.Features.ProformaInvoice;

public interface IProformaInvoiceService
{
    Task<byte[]> CreateProformaInvoicePdfAsync(ProformaInvoiceDto proformaInvoiceDto);
}
