namespace DentallApp.Features.SpecificTreatments.UseCases;

public class CreateSpecificTreatmentRequest
{
    public string Name { get; init; }
    public double Price { get; init; }
    public int GeneralTreatmentId { get; init; }

    public SpecificTreatment MapToSpecificTreatment()
    {
        return new()
        {
            Name  = Name,
            Price = Price,
            GeneralTreatmentId = GeneralTreatmentId
        };
    }
}

public class CreateSpecificTreatmentUseCase
{
    private readonly DbContext _context;

    public CreateSpecificTreatmentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreatedId>> ExecuteAsync(CreateSpecificTreatmentRequest request)
    {
        var specificTreatment = request.MapToSpecificTreatment();
        _context.Add(specificTreatment);
        await _context.SaveChangesAsync();
        return Result.CreatedResource(specificTreatment.Id);
    }
}
