namespace DentallApp.Features.GeneralTreatments.UseCases;

public class CreateGeneralTreatmentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    [Required]
    [Image]
    public IFormFile Image { get; init; }
    public int Duration { get; init; }

    public GeneralTreatment MapToGeneralTreatment()
    {
        return new()
        {
            Name        = Name,
            Description = Description,
            Duration    = Duration,
            ImageUrl    = Image.GetRandomImageName()
        };
    }
}

public class CreateGeneralTreatmentUseCase
{
    private readonly DbContext _context;
    private readonly string _basePath;

    public CreateGeneralTreatmentUseCase(DbContext context, AppSettings settings)
    {
        _context = context;
        _basePath = settings.DentalServicesImagesPath;
    }

    public async Task<Result<CreatedId>> ExecuteAsync(CreateGeneralTreatmentRequest request)
    {
        var generalTreatment = request.MapToGeneralTreatment();
        _context.Add(generalTreatment);
        await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        await _context.SaveChangesAsync();
        return Result.CreatedResource(generalTreatment.Id);
    }
}
