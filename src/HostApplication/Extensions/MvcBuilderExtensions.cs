namespace DentallApp.HostApplication.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddCustomInvalidModelStateResponse(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = (context) =>
            {
                var errors = (from state in context.ModelState
                              where state.Value.Errors.Count > 0
                              select new
                              {
                                  state.Key,
                                  ErrorMessages = state.Value.Errors.Select(modelError => modelError.ErrorMessage)
                              }).ToDictionary(x => x.Key, x => x.ErrorMessages);

                var result = new
                {
                    Success = false,
                    Message = InvalidModelStateMessage,
                    Errors  = errors
                };
                return new BadRequestObjectResult(result);
            };
        });
        return builder;
    }

    /// <summary>
    /// Adds the plugins to make it part of the application.
    /// This allows to register the controllers of each plugin in the application.
    /// </summary>
    public static IMvcBuilder AddApplicationParts(this IMvcBuilder builder) 
    {
        foreach (Assembly assembly in PluginLoader.Assemblies)
        {
            builder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
        }
        return builder;
    }
}
