namespace DentallApp.Shared.Plugin.Contracts;

/// <summary>
/// This contract is for plugins to create their own entity configurations using Entity Framework.
/// </summary>
public interface IModelCreating
{ 
    void OnModelCreating(ModelBuilder modelBuilder);
}
