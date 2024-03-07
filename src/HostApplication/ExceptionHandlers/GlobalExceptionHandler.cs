namespace DentallApp.HostApplication.ExceptionHandlers;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        Result result = Result.CriticalError(Messages.UnexpectedError);
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
