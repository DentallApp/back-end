namespace DentallApp.Shared.Services;

public interface IHtmlTemplateLoader
{
    Task<string> LoadAsync(string path, object model);
    string Load(string path, object model);
}
