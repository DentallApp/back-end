namespace DentallApp.Core.SpecificTreatments.UseCases;

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

public class CreateSpecificTreatmentValidator : AbstractValidator<CreateSpecificTreatmentRequest>
{
    public CreateSpecificTreatmentValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.GeneralTreatmentId).GreaterThan(0);
        RuleFor(request => request.Price).GreaterThan(0);
    }
}

public class CreateSpecificTreatmentUseCase(DbContext context, CreateSpecificTreatmentValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateSpecificTreatmentRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var specificTreatment = request.MapToSpecificTreatment();
        context.Add(specificTreatment);
        await context.SaveChangesAsync();
        return Result.CreatedResource(specificTreatment.Id);
    }
}
