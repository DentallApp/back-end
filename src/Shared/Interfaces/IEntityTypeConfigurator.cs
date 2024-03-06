namespace DentallApp.Shared.Interfaces;

/// <summary>
/// Allows configuration to be performed for an entity type.
/// </summary>
/// <remarks>
/// This interface is for plugins to create their own entity configurations using <see cref="Microsoft.EntityFrameworkCore"/>.
/// </remarks>
public interface IEntityTypeConfigurator
{
    /// <summary>
    /// Configures the shape of your entities, the relationships between them, and how they map to the database.
    /// </summary>
    /// <param name="modelBuilder">
    /// An instance of <see cref="ModelBuilder"/> that provides an API to perform configurations to an entity.
    /// </param>
    void ConfigureEntities(ModelBuilder modelBuilder);
}
