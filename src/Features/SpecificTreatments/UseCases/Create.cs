namespace DentallApp.Features.SpecificTreatments.UseCases;

public class CreateSpecificTreatmentRequest
{
    public string Name { get; init; }
    public double Price { get; init; }
    public int GeneralTreatmentId { get; init; }

    public SpecificTreatment MapToSpecificTreatment() => new()
    {
        Name  = Name,
        Price = Price,
        GeneralTreatmentId = GeneralTreatmentId
    };
}

public class CreateSpecificTreatmentUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateSpecificTreatmentRequest request)
    {
        var specificTreatment = request.MapToSpecificTreatment();
        context.Add(specificTreatment);
        await context.SaveChangesAsync();
        return Result.CreatedResource(specificTreatment.Id);
    }
}
