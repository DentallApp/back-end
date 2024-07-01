namespace DentallApp.HostApplication.Extensions;

public static class SwaggerGen
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AcceptLanguageHeaderParameter>();
            options.EnableAnnotations();
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
                    Array.Empty<string>()
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

    private class AcceptLanguageHeaderParameter(IConfiguration configuration) : IOperationFilter
    {
        private readonly IList<IOpenApiAny> _languageOptions = CreateLanguageOptions(configuration);
        private readonly string _defaultLanguage = configuration.GetDefaultLanguage();

        private static List<IOpenApiAny> CreateLanguageOptions(IConfiguration configuration)
        {
            var languageOptions = new List<IOpenApiAny>();
            var languages = configuration.GetLanguages();
            foreach (var language in languages)
                languageOptions.Add(new OpenApiString(language));
            return languageOptions;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                Description = "Language preference for the response.",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString(_defaultLanguage),
                    Enum = _languageOptions
                }
            });
        }
    }
}
