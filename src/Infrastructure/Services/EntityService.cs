namespace DentallApp.Infrastructure.Services;

/// <inheritdoc cref="IEntityService{TEntity}" />
public class EntityService<TEntity> 
    : IEntityService<TEntity> where TEntity : BaseEntity, IIntermediateEntity, new()
{
    private readonly IRepository<TEntity> _repository;

    public EntityService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public void Update(int key, ref List<TEntity> source, ref List<int> identifiers)
    {
        identifiers = identifiers.Distinct().Order().ToList();
        source = source.OrderBy(entity => entity.SecondaryForeignKey).ToList();

        if (source.Count == identifiers.Count)
        {
            _ = source.Zip(identifiers, (currentEntity, id) => currentEntity.SecondaryForeignKey = id).ToList();
        }
        else
        {
            foreach (TEntity currentEntity in source)
                if (identifiers.NotContains(currentEntity.SecondaryForeignKey))
                    _repository.Remove(currentEntity);

            foreach (int id in identifiers)
                if (source.NotContains(id))
                    _repository.Add(new TEntity { PrimaryForeignKey = key, SecondaryForeignKey = id });
        }
    }
}

file static class ListExtensions
{
    public static bool NotContains(this List<int> identifiers, int secondaryForeignKey)
    {
        return !identifiers.Contains(secondaryForeignKey);
    }

    public static bool NotContains<TEntity>(this List<TEntity> source, int id) where TEntity : IIntermediateEntity
    {
        return !source.Any(currentEntity => currentEntity.SecondaryForeignKey == id);
    }
}
