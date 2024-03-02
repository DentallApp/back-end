namespace DentallApp.HostApplication.Extensions;

public static class ControllersExtensions
{
    public static IServiceCollection AddControllersAsService(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.SuppressAsyncSuffixInActionNames = false;
            options.Filters.Add<TranslateResultToActionResultAttribute>();
        })
        .AddJsonOptions(jsonOpts =>
        {
            jsonOpts.JsonSerializerOptions.Converters.Add(new TimeSpanJsonConverter());
        })
        .AddCustomInvalidModelStateResponse()
        .AddApplicationParts();
        return services;
    }

    private static IMvcBuilder AddCustomInvalidModelStateResponse(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = (context) => context.ModelState.BadRequest();
        });
        return builder;
    }

    /// <summary>
    /// Adds the plugins to make it part of the application.
    /// This allows to register the controllers of each plugin in the application.
    /// </summary>
    private static IMvcBuilder AddApplicationParts(this IMvcBuilder builder) 
    {
        foreach (Assembly assembly in PluginLoader.Assemblies)
        {
            builder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
        return builder;
    }
}
