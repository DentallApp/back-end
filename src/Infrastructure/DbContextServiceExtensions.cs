using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Infrastructure;

public static class DbContextServiceExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string hostAppName)
    {
        var settings = new EnvBinder().Bind<DatabaseSettings>();
        LinqToDBForEFTools.Initialize();
        var cs = settings.DbConnectionString;
        services.AddDbContext<DbContext, AppDbContext>(options =>
        {
            options.UseMySql(cs, ServerVersion.AutoDetect(cs),
                    mySqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions
                         .EnableRetryOnFailure(
                            maxRetryCount: settings.DbMaxRetryCount,
                            maxRetryDelay: TimeSpan.FromSeconds(settings.DbMaxRetryDelay),
                            errorNumbersToAdd: null)
                         .MigrationsAssembly(hostAppName);
                    })
                   .UseSnakeCaseNamingConvention();
        });

        services
            .AddSingleton<IDbConnectionFactory>(new MariaDbConnectionFactory(settings.DbConnectionString))
            .AddScoped<IDbConnection>(serviceProvider => new MySqlConnection(settings.DbConnectionString));

        return services;
    }
}
