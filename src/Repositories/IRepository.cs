namespace DentallApp.Repositories;

public interface IRepository<TEntity> where TEntity : ModelBase
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void ChangeStateToInactive(TEntity entity);
    void Remove(TEntity entity);
}