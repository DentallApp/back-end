namespace DentallApp.HostApplication.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error ?? ex;

            _logger.LogError("[Error]: There was an internal error on the server.");
            _logger.LogError("[Exception]: {exception}", exception);

            var response = new Response
            {
                Success = false,
                Message = UnexpectedErrorMessage
            };

            if (exception is UniqueConstraintException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Errors = new[] { UniqueConstraintViolatedMessage };
            }

            await context.Response.WriteAsJsonAsync(response);
        }
    }

}

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();
}
