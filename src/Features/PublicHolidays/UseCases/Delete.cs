namespace DentallApp.Features.PublicHolidays.UseCases;

public class DeletePublicHolidayUseCase
{
    private readonly AppDbContext _context;

    public DeletePublicHolidayUseCase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Response> Execute(int id)
    {
        var holiday = await _context.Set<PublicHoliday>()
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return new Response(ResourceNotFoundMessage);

        _context.SoftDelete(holiday);
        await _context.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
