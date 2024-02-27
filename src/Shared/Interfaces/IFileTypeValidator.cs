namespace DentallApp.Shared.Interfaces;

public interface IFileTypeValidator
{
    Result IsImage(Stream fileContent);
}
