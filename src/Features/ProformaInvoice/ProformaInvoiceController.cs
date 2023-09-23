namespace DentallApp.Features.ProformaInvoice;

[Route("proforma-invoice")]
[ApiController]
public class ProformaInvoiceController : ControllerBase
{
    [AuthorizeByRole(RolesName.BasicUser)]
    [Route("pdf")]
    [HttpPost]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadProformaInvoiceRequest request,
        [FromServices]DownloadProformaInvoiceUseCase useCase)
    {
        var contents = await useCase.DownloadAsPdfAsync(request);
        return File(contents, "application/pdf", "ProformaReporte.pdf");
    }
}
