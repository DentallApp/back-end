namespace DentallApp.Helpers.HtmlConverterHelpers;

public class HtmlConverterIText : IHtmlConverter
{
    public byte[] ConvertToPdf(string html, MemoryStream pdfStream)
    {
        HtmlConverter.ConvertToPdf(html, pdfStream);
        return pdfStream.ToArray();
    }

    public void ConvertToPdf(string html, Stream pdfStream)
        => HtmlConverter.ConvertToPdf(html, pdfStream);
}
