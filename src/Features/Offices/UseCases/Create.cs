namespace DentallApp.Features.Offices.UseCases;

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

public class CreateOfficeUseCase(DbContext context)
{
    public async Task<Result<CreatedId>> ExecuteAsync(CreateOfficeRequest request)
    {
        var office = request.MapToOffice();
        context.Add(office);
        await context.SaveChangesAsync();
        return Result.CreatedResource(office.Id);
    }
}
