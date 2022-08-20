namespace DentallApp.Features.ProformaInvoice;

[Route("proforma-invoice")]
[ApiController]
public class ProformaInvoiceController : ControllerBase
{
    private readonly IProformaInvoiceService _proformaInvoiceService;

    public ProformaInvoiceController(IProformaInvoiceService proformaInvoiceService)
    {
        _proformaInvoiceService = proformaInvoiceService;
    }

    [AuthorizeByRole(RolesName.BasicUser)]
    [Route("pdf")]
    [HttpPost]
    public async Task<ActionResult> Post([FromBody]ProformaInvoiceDto proformaInvoiceDto)
    {
        var contents = await _proformaInvoiceService.CreateProformaInvoicePdfAsync(proformaInvoiceDto);
        return File(contents, "application/pdf", "ProformaReporte.pdf");
    }
}