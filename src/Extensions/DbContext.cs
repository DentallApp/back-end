using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Extensions;

public static class DbContext
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, AppSettings settings)
    {
        LinqToDBForEFTools.Initialize();
        var cs = settings.ConnectionString;
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseMySql(cs, ServerVersion.AutoDetect(cs),
                    mySqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    })
                   .UseSnakeCaseNamingConvention();
        });
        return services;
    }
}
