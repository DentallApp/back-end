namespace DentallApp.Attributes;

public class ImageAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var formFile = value as IFormFile;
        if (formFile == null)
            return ValidationResult.Success;

        var fileStream = formFile.OpenReadStream();
        bool isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);
        if (!isRecognizableType)
            return new ValidationResult(UnrecognizableFileMessage);
        bool isImage = fileStream.IsImage();
        return isImage ? ValidationResult.Success : new ValidationResult(NotAnImageMessage);
    }
}
