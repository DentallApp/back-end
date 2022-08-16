namespace DentallApp.Repositories;

public class SoftDeleteRepository<TEntity> : Repository<TEntity>, ISoftDeleteRepository<TEntity> where TEntity : ModelWithSoftDelete
{
    public SoftDeleteRepository(AppDbContext context) : base(context) { }

    public override void Delete(TEntity entity)
    {
        entity.IsDeleted = true;
        Context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
    }
}
