namespace DentallApp.DataAccess.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity> GetByIdAsync(int id);
    void Add(TEntity entity);
    void Remove(TEntity entity);
}