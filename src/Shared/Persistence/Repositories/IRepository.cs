namespace DentallApp.Shared.Persistence.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(int id);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}