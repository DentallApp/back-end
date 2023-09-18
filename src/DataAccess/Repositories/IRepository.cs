namespace DentallApp.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Insert(TEntity entity);
    void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties);
    void Update(TEntity entity);
    void SoftDelete(TEntity entity);
    void Delete(TEntity entity);
    Task<int> SaveAsync();
}