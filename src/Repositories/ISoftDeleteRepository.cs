namespace DentallApp.Repositories;

public interface ISoftDeleteRepository<TEntity> : IRepository<TEntity> where TEntity : ModelWithSoftDelete
{
}
