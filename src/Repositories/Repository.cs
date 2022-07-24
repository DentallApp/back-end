namespace DentallApp.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : ModelBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _entities;

    public Repository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _entities.ToListAsync();

    public virtual async Task<TEntity> GetByIdAsync(int id)
        => await _entities.Where(entity => entity.Id == id).FirstOrDefaultAsync();

    public virtual void Insert(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;
        _entities.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _entities.Update(entity);
    }

    public virtual void Delete(TEntity entity)
        => _entities.Remove(entity);

    public virtual Task<int> SaveAsync()
        => _context.SaveChangesAsync();
}