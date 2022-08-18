namespace DentallApp.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddCustomInvalidModelStateResponse(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = (context) =>
            {
                var errors = new Dictionary<string, IEnumerable<string>>();
                foreach (var state in context.ModelState)
                    if (state.Value.Errors.Count() > 0)
                        errors.Add(state.Key, state.Value.Errors.Select(modelError => modelError.ErrorMessage));

                var result = new Response
                {
                    Success = false,
                    Message = InvalidModelStateMessage,
                    Errors = errors
                };
                return new BadRequestObjectResult(result);
            };
        });
        return builder;
    }
}
