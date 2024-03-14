namespace DentallApp.Core.ProformaInvoice;

[Route("proforma-invoice")]
[ApiController]
public class ProformaInvoiceController
{
    /// <summary>
    /// Downloads the invoice proforma in PDF format.
    /// </summary>
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(byte[]), contentTypes: MediaTypeNames.Application.Pdf)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(Result), contentTypes: MediaTypeNames.Application.Json)]
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
