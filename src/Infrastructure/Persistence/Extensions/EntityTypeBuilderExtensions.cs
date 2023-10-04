namespace DentallApp.Infrastructure.Persistence.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> HasQueryFilterSoftDelete<TEntity>(
        this EntityTypeBuilder<TEntity> builder) where TEntity : SoftDeleteEntity
    {
        return builder.HasQueryFilter(entity => !entity.IsDeleted);
    }
}
