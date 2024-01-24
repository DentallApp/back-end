namespace DentallApp.Features.GeneralTreatments.UseCases;

public class DeleteGeneralTreatmentUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        int updatedRows = await context.SoftDeleteAsync<GeneralTreatment>(id);
        if (updatedRows == 0)
            return Result.NotFound();

        return Result.DeletedResource();
    }
}
