namespace DentallApp.HostApplication.Extensions;

public static class ScrutorExtensions
{
    /// <summary>
    /// Represents a set of conventions that the names of each type must follow.
    /// </summary>
    /// <remarks>
    /// Each type name must end with any of these conventions.
    /// </remarks>
    private static readonly string[] s_conventions = new[]
    {
        "UseCase"
    };

    /// <summary>
    /// Checks if the type has the convention at the end of its name.
    /// </summary>
    /// <param name="typeName">
    /// The name of a type (e.g., a class).
    /// </param>
    /// <returns>
    /// <c>true</c> if the type has the convention at the end of its name; otherwise, <c>false</c>.
    /// </returns>
    private static bool HasConvention(string typeName)
    {
        return s_conventions.Any(typeName.EndsWith);
    }

    /// <summary>
    /// Registers dependencies automatically using the <see cref="Scrutor" /> library.
    /// </summary>
    public static IServiceCollection RegisterAutoDependencies(this IServiceCollection services)
    {
        // Specifies the assembly for Scrutor to search for types.
        var assemblies = new List<Assembly>
        {
            typeof(GetDependentsByCurrentUserIdUseCase).Assembly,
            typeof(IGetAvailableHoursUseCase).Assembly
        };

        assemblies.AddRange(PluginLoader.Assemblies);
        services.Scan(scan => scan
        // Search the types from the specified assemblies.
            .FromAssemblies(assemblies)
              // Registers the concrete classes as a service, for example: 'CreateUserUseCase'.
              // It also registers interfaces as a service, for example: 'ICreateUserUseCase'.
              .AddClasses(classes => classes.Where(type => HasConvention(type.Name)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

        return services;
    }
}
