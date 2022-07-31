namespace DentallApp.Repositories;

public interface IRepository<TEntity> where TEntity : ModelBase
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Insert(TEntity entity);
    void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
    void Delete(TEntity entity);
    Task<int> SaveAsync();
}