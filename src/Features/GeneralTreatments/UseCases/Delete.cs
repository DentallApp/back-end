namespace DentallApp.Features.GeneralTreatments.UseCases;

public class DeleteGeneralTreatmentUseCase
{
    private readonly AppDbContext _context;

    public DeleteGeneralTreatmentUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id)
    {
        int updatedRows = await _context.SoftDeleteAsync<GeneralTreatment>(id);
        if (updatedRows == 0)
            return new Response(ResourceNotFoundMessage);

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
