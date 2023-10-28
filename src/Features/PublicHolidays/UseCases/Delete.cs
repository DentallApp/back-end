namespace DentallApp.Features.PublicHolidays.UseCases;

public class DeletePublicHolidayUseCase
{
    private readonly DbContext _context;

    public DeletePublicHolidayUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<Result> ExecuteAsync(int id)
    {
        var holiday = await _context.Set<PublicHoliday>()
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return Result.NotFound();

        _context.SoftDelete(holiday);
        await _context.SaveChangesAsync();
        return Result.DeletedResource();
    }
}
