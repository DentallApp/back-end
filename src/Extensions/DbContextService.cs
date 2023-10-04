using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Extensions;

public static class DbContextService
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, DatabaseSettings settings)
    {
        LinqToDBForEFTools.Initialize();
        var cs = settings.DbConnectionString;
        services.AddDbContext<DbContext, AppDbContext>(options =>
        {
            options.UseMySql(cs, ServerVersion.AutoDetect(cs),
                    mySqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount:     settings.DbMaxRetryCount,
                            maxRetryDelay:     TimeSpan.FromSeconds(settings.DbMaxRetryDelay),
                            errorNumbersToAdd: null);
                    })
                   .UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
