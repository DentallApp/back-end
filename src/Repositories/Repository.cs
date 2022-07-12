namespace DentallApp.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : ModelBase
{
    private readonly DbSet<TEntity> _entities;

    public Repository(AppDbContext context)
    {
        _entities = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _entities.Where(entity => entity.State).ToListAsync();

    public async Task<TEntity> GetByIdAsync(int id)
        => await _entities.Where(entity => entity.State && entity.Id == id).FirstOrDefaultAsync();

    public void Add(TEntity entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = entity.CreatedAt;
        _entities.Add(entity);
    }

    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        _entities.Update(entity);
    }

    public void ChangeStateToInactive(TEntity entity)
    {
        entity.State = false;
        Update(entity);
    }

    public void Remove(TEntity entity)
        => _entities.Remove(entity);
}