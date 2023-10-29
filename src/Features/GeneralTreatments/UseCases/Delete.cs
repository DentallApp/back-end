namespace DentallApp.Features.GeneralTreatments.UseCases;

public class DeleteGeneralTreatmentUseCase
{
    private readonly DbContext _context;

    public DeleteGeneralTreatmentUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(int id)
    {
        int updatedRows = await _context.SoftDeleteAsync<GeneralTreatment>(id);
        if (updatedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
