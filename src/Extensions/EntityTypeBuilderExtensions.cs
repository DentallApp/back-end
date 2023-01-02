namespace DentallApp.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> HasQueryFilterSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : SoftDeleteEntity
        => builder.HasQueryFilter(entity => !entity.IsDeleted);
}
