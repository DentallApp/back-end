using iText.Kernel.Geom;
using iText.Kernel.Pdf;

namespace DentallApp.Infrastructure.Services;

public class HtmlConverterIText : IHtmlConverter
{
    private byte[] ConvertToPdf(string html, MemoryStream pdfStream, PageSize pageSize)
    {
        var pdfDocument = new PdfDocument(new PdfWriter(pdfStream));
        pdfDocument.SetDefaultPageSize(new PageSize(pageSize));
        HtmlConverter.ConvertToPdf(html, pdfDocument, null);
        return pdfStream.ToArray();
    }

    public byte[] ConvertToPdf(string html, MemoryStream pdfStream)
    {
        HtmlConverter.ConvertToPdf(html, pdfStream);
        return pdfStream.ToArray();
    }

    public void ConvertToPdf(string html, Stream pdfStream)
        => HtmlConverter.ConvertToPdf(html, pdfStream);

    public byte[] ConvertToPdfWithPageSizeA3(string html, MemoryStream pdfStream)
        => ConvertToPdf(html, pdfStream, PageSize.A3);
}
