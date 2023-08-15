namespace DentallApp.Features.GeneralTreatments.UseCases;

public class CreateGeneralTreatmentRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    [Required]
    [Image]
    public IFormFile Image { get; init; }
    public int Duration { get; init; }
}

public static class CreateGeneralTreatmentMapper
{
    public static GeneralTreatment MapToGeneralTreatment(this CreateGeneralTreatmentRequest request)
    {
        return new()
        {
            Name        = request.Name,
            Description = request.Description,
            Duration    = request.Duration,
            ImageUrl    = request.Image.GetRandomImageName()
        };
    }
}

public class CreateGeneralTreatmentUseCase
{
    private readonly AppDbContext _context;
    private readonly string _basePath;

    public CreateGeneralTreatmentUseCase(AppDbContext context, AppSettings settings)
    {
        _context = context;
        _basePath = settings.DentalServicesImagesPath;
    }

    public async Task<Response<InsertedIdDto>> Execute(CreateGeneralTreatmentRequest request)
    {
        var generalTreatment = request.MapToGeneralTreatment();
        _context.Add(generalTreatment);
        await request.Image.WriteAsync(Path.Combine(_basePath, generalTreatment.ImageUrl));
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = generalTreatment.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
