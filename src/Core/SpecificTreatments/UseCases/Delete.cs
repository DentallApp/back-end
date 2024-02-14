namespace DentallApp.Core.SpecificTreatments.UseCases;

public class DeleteSpecificTreatmentUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        int deletedRows = await context.Set<SpecificTreatment>()
            .Where(treatment => treatment.Id == id)
            .ExecuteDeleteAsync();

        if (deletedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
