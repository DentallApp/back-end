namespace DentallApp.Infrastructure.Services;

public class HtmlTemplateLoaderScriban : IHtmlTemplateLoader
{
    public string Load(string path, object model)
    {
        var html = File.ReadAllText(path);
        var template = Template.Parse(html);
        var result = template.Render(model);
        return result;
    }

    public async Task<string> LoadAsync(string path, object model)
    {
        var html = await File.ReadAllTextAsync(path);
        var template = Template.Parse(html);
        var result = await template.RenderAsync(model);
        return result;
    }
}
