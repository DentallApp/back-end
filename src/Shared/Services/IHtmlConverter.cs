namespace DentallApp.Shared.Services;

public interface IHtmlConverter
{
    void ConvertToPdf(string html, Stream pdfStream);
    byte[] ConvertToPdf(string html, MemoryStream pdfStream);
    byte[] ConvertToPdfWithPageSizeA3(string html, MemoryStream pdfStream);
}
