namespace DentallApp.Core.GeneralTreatments.UseCases;

public class CreateGeneralTreatmentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public IFormFile Image { get; init; }
    public int Duration { get; init; }

    public GeneralTreatment MapToGeneralTreatment() => new()
    {
        Name        = Name,
        Description = Description,
        Duration    = Duration,
        ImageUrl    = Image.GetRandomImageName()
    };
}

public class CreateGeneralTreatmentValidator : AbstractValidator<CreateGeneralTreatmentRequest>
{
    public CreateGeneralTreatmentValidator(IFileTypeValidator fileTypeValidator)
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Image)
            .NotEmpty()
            .MustBeValidImage(fileTypeValidator);
        RuleFor(request => request.Duration).GreaterThan(0);
    }
}

public class CreateGeneralTreatmentUseCase(
    DbContext context, 
    AppSettings settings,
    CreateGeneralTreatmentValidator validator)
{
    private readonly string _basePath = settings.DentalServicesImagesPath;

    public async Task<Result<CreatedId>> ExecuteAsync(CreateGeneralTreatmentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var generalTreatment = request.MapToGeneralTreatment();
        context.Add(generalTreatment);
        await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        await context.SaveChangesAsync();
        return Result.CreatedResource(generalTreatment.Id);
    }
}
