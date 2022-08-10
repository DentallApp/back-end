namespace DentallApp.Features.Chatbot.Factories;

public class RepositoriesFactory : IRepositoriesFactory
{
    private readonly AppDbContext _context;

    public RepositoriesFactory(AppDbContext context)
    {
        _context = context;
    }

    public IBotQueryRepository CreateBotQueryRepository()
        => new BotQueryRepository(_context);
}
