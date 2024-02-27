namespace DentallApp.Core.SpecificTreatments.UseCases;

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

public class UpdateSpecificTreatmentValidator : AbstractValidator<UpdateSpecificTreatmentRequest>
{
    public UpdateSpecificTreatmentValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.GeneralTreatmentId).GreaterThan(0);
        RuleFor(request => request.Price).GreaterThan(0);
    }
}

public class UpdateSpecificTreatmentUseCase(DbContext context, UpdateSpecificTreatmentValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdateSpecificTreatmentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

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
