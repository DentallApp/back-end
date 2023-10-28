namespace DentallApp.Features.Offices.UseCases;

public class CreateOfficeRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }

    public Office MapToOffice()
    {
        return new()
        {
            Name          = Name,
            Address       = Address,
            ContactNumber = ContactNumber
        };
    }
}

public class CreateOfficeUseCase
{
    private readonly DbContext _context;

    public CreateOfficeUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreatedId>> ExecuteAsync(CreateOfficeRequest request)
    {
        var office = request.MapToOffice();
        _context.Add(office);
        await _context.SaveChangesAsync();
        return Result.CreatedResource(office.Id);
    }
}
