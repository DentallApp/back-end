namespace DentallApp.DataAccess;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _entities;

    protected AppDbContext Context => _context;
    protected DbSet<TEntity> Entities => _entities;

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
        => _entities.Add(entity);

    public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
    {
        var entityEntry = _context.Entry(entity);
        foreach (var property in properties)
            entityEntry.Property(property).IsModified = true;
    }
    
    public virtual void Update(TEntity entity)
        => _entities.Update(entity);

    public virtual void Delete(TEntity entity)
        => _entities.Remove(entity);

    public virtual void SoftDelete(TEntity entity)
    {
        if (entity is SoftDeleteEntity softDeleteEntity)
        {
            softDeleteEntity.IsDeleted = true;
            Context.Entry(softDeleteEntity).Property(e => e.IsDeleted).IsModified = true;
        }
        else
        {
            throw new Exception($"{typeof(TEntity).FullName} must be a subtype of {nameof(SoftDeleteEntity)} to call the {nameof(SoftDelete)} method.");
        }
    }

    public virtual Task<int> SaveAsync()
        => _context.SaveChangesAsync();

    public virtual async Task<IAppDbContextTransaction> BeginTransactionAsync()
        => new AppDbContextTransactionEFCore(await _context.Database.BeginTransactionAsync());
}