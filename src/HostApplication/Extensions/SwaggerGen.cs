namespace DentallApp.HostApplication.Extensions;

public static class SwaggerGen
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "DentallApi", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Description = "Please insert JWT token into field"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            var coreAssemblyName = typeof(GetDependentsByCurrentUserIdUseCase).Assembly.GetName().Name;
            var filePath = Path.Combine(AppContext.BaseDirectory, coreAssemblyName + ".xml");
            options.IncludeXmlComments(filePath);

            foreach(Assembly assembly in PluginLoader.Assemblies) 
            {
                var pluginName = assembly.GetName().Name;
                var basePath = Path.Combine(AppContext.BaseDirectory, "plugins", pluginName);
                var pluginPath = Path.Combine(basePath, pluginName + ".xml");
                options.IncludeXmlComments(pluginPath);
            }
        });
        return services;
    }
}
