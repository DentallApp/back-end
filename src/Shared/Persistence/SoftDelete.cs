namespace DentallApp.Shared.Persistence;

public static class SoftDeleteExtensions
{
    public static void SoftDelete<TEntity>(
        this DbContext context, 
        TEntity entityToDelete) where TEntity : SoftDeleteEntity
    {
        entityToDelete.IsDeleted = true;
        context.Entry(entityToDelete).Property(e => e.IsDeleted).IsModified = true;
    }

    public static async Task<int> SoftDeleteAsync<TEntity>(
        this DbContext context,
        int entityId) where TEntity : SoftDeleteEntity
    {
        int updatedRows = await context.Set<TEntity>()
                   .Where(e => e.Id == entityId)
                   .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true));

        return updatedRows;
    }
}
