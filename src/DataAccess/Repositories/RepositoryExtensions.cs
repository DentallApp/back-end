namespace DentallApp.DataAccess.Repositories;

/// <summary>
/// Represents the extensions of <see cref="IRepository{TEntity}" />.
/// </summary>
public static class RepositoryExtensions
{
    /// <summary>
    /// Adds, updates or deletes an entity of type <typeparamref name="TEntity" />.
    /// </summary>
    /// <remarks>
    /// <para>This method is used in several parts of the code, such as, for example, in <see cref="UserRoleRepository.UpdateUserRoles" />.</para>
    /// It is not recommended to use this method in classes that represent business logic, because it can complicate unit tests.
    /// </remarks>
    /// <typeparam name="TEntity">The entity to update.</typeparam>
    /// <param name="repository">An entity repository.</param>
    /// <param name="key">
    /// The primary foreign key of <typeparamref name="TEntity" />.
    /// </param>
    /// <param name="source">
    /// A sequence of entities of type <typeparamref name="TEntity" /> loaded from a data source (e.g, database).
    /// </param>
    /// <param name="identifiers">
    /// A sequence of foreign keys of type <see cref="int" /> obtained from a client (e.g, web browser).
    /// </param>
    public static void AddOrUpdateOrDelete<TEntity>(this IRepository<TEntity> repository, int key, ref List<TEntity> source, ref List<int> identifiers)
        where TEntity : EntityBase, IIntermediateEntity, new()
    {
        identifiers = identifiers.Distinct().OrderBy(id => id).ToList();
        source     = source.OrderBy(entity => entity.SecondaryForeignKey).ToList();

        if (source.Count == identifiers.Count)
        {
            _ = source.Zip(identifiers, (currentEntity, id) => currentEntity.SecondaryForeignKey = id).ToList();
        }
        else
        {
            foreach (TEntity currentEntity in source)
                if(identifiers.NotContains(currentEntity.SecondaryForeignKey))
                    repository.Delete(currentEntity);

            foreach (int id in identifiers)
                if(source.NotContains(id))
                    repository.Insert(new TEntity { PrimaryForeignKey = key, SecondaryForeignKey = id });
        }
    }

    private static bool NotContains(this List<int> identifiers, int secondaryForeignKey) 
        => !identifiers.Contains(secondaryForeignKey);

    private static bool NotContains<TEntity>(this List<TEntity> source, int id) where TEntity : IIntermediateEntity
        => !source.Any(currentEntity => currentEntity.SecondaryForeignKey == id);
}
