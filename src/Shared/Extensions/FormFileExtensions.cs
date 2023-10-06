namespace DentallApp.Shared.Extensions;

public static class FormFileExtensions
{
    public static async Task WriteAsync(this IFormFile file, string path, FileMode fileMode = FileMode.Create)
    {
        using var stream = new FileStream(path, fileMode);
        await file.CopyToAsync(stream);
    }

    public static string GetFileNameWithoutExtension(this IFormFile file)
        => Path.GetFileNameWithoutExtension(file.FileName);

    public static string GetExtension(this IFormFile file)
        => Path.GetExtension(file.FileName);

    /// <summary>
    /// Gets a random name for an image without white-spaces.
    /// </summary>
    /// <param name="file"></param>
    /// <returns>
    /// <para>A random name for an image without white-spaces.</para>
    /// Each white-space will be replaced by an underscore.
    /// </returns>
    public static string GetRandomImageName(this IFormFile file)
	{
		var fileNameWithoutWhiteSpaces = file.GetFileNameWithoutExtension().Replace(" ", "_");
		return $"{fileNameWithoutWhiteSpaces}_{Guid.NewGuid()}{file.GetExtension()}";
	}
}
