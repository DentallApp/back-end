namespace DentallApp.Features.SpecificTreatments.UseCases;

public class UpdateSpecificTreatmentRequest
{
    public string Name { get; init; }
    public double Price { get; init; }
    public int GeneralTreatmentId { get; init; }

    public void MapToSpecificTreatment(SpecificTreatment treatment)
    {
        treatment.Name  = Name;
        treatment.Price = Price;
        treatment.GeneralTreatmentId = GeneralTreatmentId;
    }
}

public class UpdateSpecificTreatmentUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int id, UpdateSpecificTreatmentRequest request)
    {
        var specificTreatment = await context.Set<SpecificTreatment>()
            .Where(treatment => treatment.Id == id)
            .FirstOrDefaultAsync();

        if (specificTreatment is null)
            return Result.NotFound();

        request.MapToSpecificTreatment(specificTreatment);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }
}
