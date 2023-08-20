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

public class UpdateSpecificTreatmentUseCase
{
    private readonly AppDbContext _context;

    public UpdateSpecificTreatmentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id, UpdateSpecificTreatmentRequest request)
    {
        var specificTreatment = await _context.Set<SpecificTreatment>()
            .Where(treatment => treatment.Id == id)
            .FirstOrDefaultAsync();

        if (specificTreatment is null)
            return new Response(ResourceNotFoundMessage);

        request.MapToSpecificTreatment(specificTreatment);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
