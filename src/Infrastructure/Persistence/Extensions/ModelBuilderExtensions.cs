namespace DentallApp.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Adds the entity as a model so that EF Core can generate the database schema (tables, primary keys, foreign keys, among others).
    /// </summary>
    /// <typeparam name="TEntity">The entity type to be added.</typeparam>
    /// <param name="modelBuilder"></param>
    /// <returns></returns>
    public static ModelBuilder AddEntity<TEntity>(this ModelBuilder modelBuilder) where TEntity : class
    {
        modelBuilder.Entity<TEntity>();
        return modelBuilder;
    }

    public static DataBuilder<TEntity> AddSeedData<TEntity>(
        this ModelBuilder builder, params TEntity[] entities) where TEntity : class
    {
        foreach (var entity in entities)
        {
            if (entity is IAuditableEntity auditableEntity)
            {
                auditableEntity.CreatedAt = DateTime.Now;
                auditableEntity.UpdatedAt = DateTime.Now;
            }
        }
        return builder.Entity<TEntity>().HasData(entities);
    }
}
