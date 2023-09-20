namespace DentallApp.Infrastructure.Persistence.Extensions;

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
        int entityId) where TEntity : SoftDeleteEntity
    {
        int updatedRows = await context.Set<TEntity>()
                   .Where(e => e.Id == entityId)
                   .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true));

        return updatedRows;
    }
}
