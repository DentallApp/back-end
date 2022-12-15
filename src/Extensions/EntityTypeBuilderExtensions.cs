namespace DentallApp.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<TEntity> HasQueryFilterSoftDelete<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : SoftDeleteEntity
        => builder.HasQueryFilter(entity => !entity.IsDeleted);

    public static DataBuilder<TEntity> AddSeedData<TEntity>(this EntityTypeBuilder<TEntity> builder, params TEntity[] data) where TEntity : EntityBase
    {
        foreach(var entity in data)
        {
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;
        }
        return builder.HasData(data);
    }
}
