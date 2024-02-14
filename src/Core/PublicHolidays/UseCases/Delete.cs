namespace DentallApp.Core.PublicHolidays.UseCases;

public class DeletePublicHolidayUseCase(DbContext context)
{
    public async Task<Result> ExecuteAsync(int id)
    {
        var holiday = await context.Set<PublicHoliday>()
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return Result.NotFound();

        context.SoftDelete(holiday);
        await context.SaveChangesAsync();
        return Result.DeletedResource();
    }
}
