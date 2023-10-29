namespace DentallApp.Features.SpecificTreatments.UseCases;

public class DeleteSpecificTreatmentUseCase
{
    private readonly DbContext _context;

    public DeleteSpecificTreatmentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(int id)
    {
        int deletedRows = await _context.Set<SpecificTreatment>()
            .Where(treatment => treatment.Id == id)
            .ExecuteDeleteAsync();

        if (deletedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
