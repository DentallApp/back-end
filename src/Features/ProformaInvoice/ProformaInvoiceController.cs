namespace DentallApp.Features.ProformaInvoice;

[Route("proforma-invoice")]
[ApiController]
public class ProformaInvoiceController : ControllerBase
{
    [AuthorizeByRole(RolesName.BasicUser)]
    [Route("pdf")]
    [HttpPost]
    public async Task<ActionResult> CreatePdf(
        [FromBody]ProformaInvoiceRequest request,
        [FromServices]ProformaInvoiceService service)
    {
        var contents = await service.CreateProformaInvoiceToPdf(request);
        return File(contents, "application/pdf", "ProformaReporte.pdf");
    }
}