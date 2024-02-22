using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Infrastructure;

public static class DbContextServiceExtensions
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, string hostAppName)
    {
        LinqToDBForEFTools.Initialize();
        var settings = new EnvBinder().Bind<DatabaseSettings>();
        var connectionString = CreateDbConnectionString(settings);
        services.AddDbContext<DbContext, AppDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
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
            .AddSingleton<IDbConnectionFactory>(new MariaDbConnectionFactory(connectionString))
            .AddScoped<IDbConnection>(serviceProvider => new MySqlConnection(connectionString));

        return services;
    }

    private static string CreateDbConnectionString(DatabaseSettings settings)
    {
        var builder = new MySqlConnectionStringBuilder
        {
            UserID   = settings.DbUserName, 
            Password = settings.DbPassword,
            Server   = settings.DbHost,
            Port     = settings.DbPort,
            Database = settings.DbDatabase
        };

        return builder.ConnectionString;
    }
}
