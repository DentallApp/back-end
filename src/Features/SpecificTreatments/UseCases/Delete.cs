namespace DentallApp.Features.SpecificTreatments.UseCases;

public class DeleteSpecificTreatmentUseCase
{
    private readonly AppDbContext _context;

    public DeleteSpecificTreatmentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id)
    {
        int updatedRows = await _context.Set<SpecificTreatment>()
            .Where(treatment => treatment.Id == id)
            .ExecuteDeleteAsync();

        if (updatedRows == 0)
            return new Response(ResourceNotFoundMessage);

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
