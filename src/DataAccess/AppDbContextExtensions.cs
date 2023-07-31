namespace DentallApp.DataAccess;

public static class AppDbContextExtensions
{
    public static void SoftDelete<TEntity>(
        this AppDbContext context, 
        TEntity entityToDelete) where TEntity : SoftDeleteEntity
    {
        entityToDelete.IsDeleted = true;
        context.Entry(entityToDelete).Property(e => e.IsDeleted).IsModified = true;
    }

    public static async Task<int> SoftDeleteAsync<TEntity>(
        this AppDbContext context,
        TEntity entityToDelete) where TEntity : SoftDeleteEntity
    {
        int updatedRows = await context.Set<TEntity>()
                   .Where(e => e.Id == entityToDelete.Id)
                   .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true));

        return updatedRows;
    }
}
