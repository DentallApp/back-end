namespace DentallApp.DataAccess.Repositories;

/// <summary>
/// Represents the extensions of <see cref="IRepository{TEntity}" />.
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    /// Updates one or more entities of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <remarks>
    /// <para>This method is used in several parts of the code, such as, for example, in <see cref="UserRoleRepository.UpdateUserRoles" />.</para>
    /// It is not recommended to use this method in classes that represent business logic, because the name of this method is very technical.
    /// </remarks>
    /// <typeparam name="TEntity">The entity to update.</typeparam>
    /// <param name="repository">An entity repository.</param>
    /// <param name="key">
    /// The primary foreign key of <typeparamref name="TEntity" />.
    /// </param>
    /// <param name="source">
    /// A sequence of entities of type <typeparamref name="TEntity" /> loaded from a data source (e.g, database).
    /// </param>
    /// <param name="newEntries">
    /// A sequence of foreign keys of type <see cref="int" /> obtained from a client (e.g, web browser).
    /// </param>
    public static void UpdateEntities<TEntity>(this IRepository<TEntity> repository, int key, ref List<TEntity> source, ref List<int> newEntries)
        where TEntity : EntityBase, IIntermediateEntity, new()
    {
        newEntries = newEntries.Distinct().OrderBy(id => id).ToList();
        source     = source.OrderBy(entity => entity.SecondaryForeignKey).ToList();

        if (source.Count == newEntries.Count)
        {
            _ = source.Zip(newEntries, (currentEntity, newId) => currentEntity.SecondaryForeignKey = newId).ToList();
        }
        else
        {
            foreach (TEntity currentEntity in source)
                if(newEntries.NotContains(currentEntity.SecondaryForeignKey))
                    repository.Delete(currentEntity);

            foreach (int newId in newEntries)
                if(source.NotContains(newId))
                    repository.Insert(new TEntity { PrimaryForeignKey = key, SecondaryForeignKey = newId });
        }
    }

    private static bool NotContains(this List<int> newEntries, int secondaryForeignKey) 
        => !newEntries.Contains(secondaryForeignKey);

    private static bool NotContains<TEntity>(this List<TEntity> source, int newId) where TEntity : IIntermediateEntity
        => !source.Any(currentEntity => currentEntity.SecondaryForeignKey == newId);
}
