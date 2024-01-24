namespace DentallApp.Infrastructure.Persistence.Repositories;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> GetByIdAsync(int id)
    {
        return context.Set<TEntity>()
            .Where(entity => entity.Id == id)
            .FirstOrDefaultAsync();
    }

    public void Add(TEntity entity)
    {
        context.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Remove(entity);
    }
}
