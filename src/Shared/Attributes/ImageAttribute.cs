namespace DentallApp.Shared.Attributes;

public class ImageAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var formFile = value as IFormFile;

        // This is so that the image can be optional when updating an entity.
        if (formFile is null)
            return ValidationResult.Success;

        using var fileStream = formFile.OpenReadStream();
        bool isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);
        if (!isRecognizableType)
            return new ValidationResult(UnrecognizableFileMessage);
        bool isImage = fileStream.IsImage();
        return isImage ? ValidationResult.Success : new ValidationResult(NotAnImageMessage);
    }
}
