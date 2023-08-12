namespace DentallApp.Features.Offices.UseCases;

public class CreateOfficeRequest
{
    public string Name { get; init; }
    public string Address { get; init; }
    public string ContactNumber { get; init; }
}

public static class CreateOfficeMapper
{
    public static Office MapToOffice(this CreateOfficeRequest request)
    {
        return new()
        {
            Name          = request.Name,
            Address       = request.Address,
            ContactNumber = request.ContactNumber
        };
    }
}

public class CreateOfficeUseCase
{
    private readonly AppDbContext _context;

    public CreateOfficeUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> Execute(CreateOfficeRequest request)
    {
        var office = request.MapToOffice();
        _context.Add(office);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = office.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
