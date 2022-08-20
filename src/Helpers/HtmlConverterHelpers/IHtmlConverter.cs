namespace DentallApp.Helpers.HtmlConverterHelpers;

public interface IHtmlConverter
{
    void ConvertToPdf(string html, Stream pdfStream);
    byte[] ConvertToPdf(string html, MemoryStream pdfStream);
}
