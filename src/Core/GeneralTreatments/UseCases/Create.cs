namespace DentallApp.Core.GeneralTreatments.UseCases;

public class CreateGeneralTreatmentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    [Required]
    [Image]
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

public class CreateGeneralTreatmentUseCase(DbContext context, AppSettings settings)
{
    private readonly string _basePath = settings.DentalServicesImagesPath;

    public async Task<Result<CreatedId>> ExecuteAsync(CreateGeneralTreatmentRequest request)
    {
        var generalTreatment = request.MapToGeneralTreatment();
        context.Add(generalTreatment);
        await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        await context.SaveChangesAsync();
        return Result.CreatedResource(generalTreatment.Id);
    }
}
