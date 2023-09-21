namespace DentallApp.Shared.Services;

/// <summary>
/// Represents an entity service for CRUD operations.
/// </summary>
public interface IEntityService<TEntity> where TEntity : EntityBase, IIntermediateEntity, new()
{
    /// <summary>
    /// Updates the state of an entity.
    /// </summary>
    /// <param name="key">
    /// The primary foreign key of <typeparamref name="TEntity" />.
    /// </param>
    /// <param name="source">
    /// A sequence of entities of type <typeparamref name="TEntity" /> loaded from a data source (e.g, database).
    /// </param>
    /// <param name="identifiers">
    /// A sequence of foreign keys of type <see cref="int" /> obtained from a client (e.g, web browser).
    /// </param>
    void Update(int key, ref List<TEntity> source, ref List<int> identifiers);
}
