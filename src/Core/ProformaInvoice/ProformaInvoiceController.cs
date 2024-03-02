namespace DentallApp.Core.ProformaInvoice;

[Route("proforma-invoice")]
[ApiController]
public class ProformaInvoiceController
{
    [AuthorizeByRole(RoleName.BasicUser)]
    [Route("pdf")]
    [HttpPost]
    public async Task<ActionResult> DownloadAsPdf(
        [FromBody]DownloadProformaInvoiceRequest request,
        DownloadProformaInvoiceUseCase useCase)
    {
        return (await useCase.DownloadAsPdfAsync(request))
            .ToActionResult()
            .Result;
    }
}
