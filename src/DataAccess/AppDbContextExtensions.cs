namespace DentallApp.DataAccess;

public static class AppDbContextExtensions
{
    public static void SoftDelete<TEntity>(
        this AppDbContext context, 
        TEntity entity) where TEntity : SoftDeleteEntity
    {
        entity.IsDeleted = true;
        context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
    }
}
