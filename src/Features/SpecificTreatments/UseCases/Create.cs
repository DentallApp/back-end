namespace DentallApp.Features.SpecificTreatments.UseCases;

public class CreateSpecificTreatmentRequest
{
    public string Name { get; init; }
    public double Price { get; init; }
    public int GeneralTreatmentId { get; init; }
}

public static class CreateSpecificTreatmentMapper
{
    public static SpecificTreatment MapToSpecificTreatment(this CreateSpecificTreatmentRequest request)
    {
        return new()
        {
            Name  = request.Name,
            Price = request.Price,
            GeneralTreatmentId = request.GeneralTreatmentId
        };
    }
}

public class CreateSpecificTreatmentUseCase
{
    private readonly AppDbContext _context;

    public CreateSpecificTreatmentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<InsertedIdDto>> Execute(CreateSpecificTreatmentRequest request)
    {
        var specificTreatment = request.MapToSpecificTreatment();
        _context.Add(specificTreatment);
        await _context.SaveChangesAsync();

        return new Response<InsertedIdDto>
        {
            Data    = new InsertedIdDto { Id = specificTreatment.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }
}
