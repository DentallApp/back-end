namespace DentallApp.Core.Offices.UseCases;

public class CreateOfficeRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }

    public Office MapToOffice() => new()
    {
        Name          = Name,
        Address       = Address,
        ContactNumber = ContactNumber
    };
}

public class CreateOfficeValidator : AbstractValidator<CreateOfficeRequest>
{
    public CreateOfficeValidator()
    {
        RuleFor(request => request.Name).NotEmpty();
        RuleFor(request => request.Address).NotEmpty();
    }
}

public class CreateOfficeUseCase(DbContext context, CreateOfficeValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateOfficeRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var office = request.MapToOffice();
        context.Add(office);
        await context.SaveChangesAsync();
        return Result.CreatedResource(office.Id);
    }
}
