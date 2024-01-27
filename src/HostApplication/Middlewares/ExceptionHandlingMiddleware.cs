namespace DentallApp.HostApplication.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error ?? ex;

            logger.LogError("[Error]: There was an internal error on the server.");
            logger.LogError("[Exception]: {exception}", exception);

            Result result = Result.CriticalError(Messages.UnexpectedError);
            if (exception is UniqueConstraintException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var errors = new[] { Messages.UniqueConstraintViolated };
                result = Result.CriticalError(Messages.UnexpectedError, errors);
            }

            await context.Response.WriteAsJsonAsync(result);
        }
    }

}

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();
}
