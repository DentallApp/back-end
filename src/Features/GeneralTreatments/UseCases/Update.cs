namespace DentallApp.Features.GeneralTreatments.UseCases;

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

public class UpdateGeneralTreatmentUseCase
{
    private readonly AppDbContext _context;
    private readonly string _basePath;

    public UpdateGeneralTreatmentUseCase(AppDbContext context, AppSettings settings)
    {
        _context = context;
        _basePath = settings.DentalServicesImagesPath;
    }

    public async Task<Response> ExecuteAsync(int id, UpdateGeneralTreatmentRequest request)
    {
        var generalTreatment = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == id)
            .FirstOrDefaultAsync();

        if (generalTreatment is null)
            return new Response(ResourceNotFoundMessage);

        var oldImageUrl = generalTreatment.ImageUrl;
        request.MapToGeneralTreatment(generalTreatment);
        if (request.Image is not null)
        {
            File.Delete(Path.Combine(_basePath, oldImageUrl));
            await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        }
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
