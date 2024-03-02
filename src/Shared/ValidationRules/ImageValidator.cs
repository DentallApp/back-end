namespace DentallApp.Shared.ValidationRules;

public static class ImageValidator
{
    public static IRuleBuilderOptions<T, IFormFile> MustBeValidImage<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder, 
        IFileTypeValidator fileTypeValidator)
    {
        return ruleBuilder.Must((rootObject, formFile, context) =>
        {
            // This is so that the image can be optional when updating an entity.
            if (formFile is null)
                return true;

            using var fileStream = formFile.OpenReadStream();
            Result result = fileTypeValidator.IsImage(fileStream);
            if (result.IsSuccess)
                return true;

            context.AddFailure(result.Message);
            return false;
        });
    }
}
