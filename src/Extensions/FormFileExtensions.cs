namespace DentallApp.Extensions;

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

    private static string GetRandomImageName(this IFormFile file)
        => $"{file.GetFileNameWithoutExtension()}_{Guid.NewGuid()}{file.GetExtension()}";

    public static string GetNewPathForDentalServiceImage(this IFormFile file)
        => Path.Combine(EnvReader.Instance[AppSettings.DentalServicesImagesPath], file.GetRandomImageName());
}
