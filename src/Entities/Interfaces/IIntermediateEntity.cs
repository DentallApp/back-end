namespace DentallApp.Entities.Interfaces;

/// <summary>
/// <para>Represents an intermediate table with foreign keys.</para>
/// This interface is used in the <c>where</c> clause of <see cref="RepositoryExtensions.UpdateEntities" />.
/// </summary>
public interface IIntermediateEntity
{
    /// <summary>
    /// Gets or sets the primary foreign key.
    /// </summary>
    int PrimaryForeignKey { get; set; }

    /// <summary>
    /// Gets or sets the secondary foreign key.
    /// </summary>
    int SecondaryForeignKey { get; set; }
}
