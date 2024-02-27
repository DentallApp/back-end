namespace DentallApp.Core.GeneralTreatments.UseCases;

public class UpdateGeneralTreatmentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    [Image]
    public IFormFile Image { get; init; }
    public int Duration { get; init; }

    public void MapToGeneralTreatment(GeneralTreatment treatment)
    {
        treatment.Name        = Name;
        treatment.Description = Description;
        treatment.Duration    = Duration;
        treatment.ImageUrl    = Image is null ? treatment.ImageUrl : Image.GetRandomImageName();
    }
}

public class UpdateGeneralTreatmentValidator : AbstractValidator<UpdateGeneralTreatmentRequest>
{
    public UpdateGeneralTreatmentValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Duration).GreaterThan(0);
    }
}

public class UpdateGeneralTreatmentUseCase(
    DbContext context, 
    AppSettings settings,
    UpdateGeneralTreatmentValidator validator)
{
    private readonly string _basePath = settings.DentalServicesImagesPath;

    public async Task<Result> ExecuteAsync(int id, UpdateGeneralTreatmentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var generalTreatment = await context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == id)
            .FirstOrDefaultAsync();

        if (generalTreatment is null)
            return Result.NotFound();

        var oldImageUrl = generalTreatment.ImageUrl;
        request.MapToGeneralTreatment(generalTreatment);
        if (request.Image is not null)
        {
            File.Delete(Path.Combine(_basePath, oldImageUrl));
            await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        }
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
