namespace DentallApp.Features.GeneralTreatments.UseCases;

public class GetGeneralTreatmentByIdResponse
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string ImageUrl { get; init; }
}

public class GetGeneralTreatmentByIdUseCase
{
    private readonly AppDbContext _context;

    public GetGeneralTreatmentByIdUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response<GetGeneralTreatmentByIdResponse>> Execute(int id)
    {
        var generalTreatment = await _context.Set<GeneralTreatment>()
            .Where(treatment => treatment.Id == id)
            .Select(treatment => new GetGeneralTreatmentByIdResponse
            {
                Name        = treatment.Name,
                Description = treatment.Description,
                ImageUrl    = treatment.ImageUrl
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (generalTreatment is null)
            return new Response<GetGeneralTreatmentByIdResponse>(ResourceNotFoundMessage);

        return new Response<GetGeneralTreatmentByIdResponse>()
        {
            Success = true,
            Data    = generalTreatment,
            Message = GetResourceMessage
        };
    }
}
