namespace DentallApp.Infrastructure.Services;

public class FileTypeService : IFileTypeValidator
{
    public Result IsImage(Stream fileContent)
    {
        bool isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileContent);
        if (!isRecognizableType)
            return Result.Failure(Messages.UnrecognizableFile);

        return fileContent.IsImage() ? 
            Result.Success() : 
            Result.Failure(Messages.IsNotImage);
    }
}
