namespace DentallApp.HostApplication.ExceptionHandlers;

public class UniqueConstraintExceptionHandler(
    ILogger<UniqueConstraintExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not UniqueConstraintException)
            return false;

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        Result result = Result.Invalid(Messages.UniqueConstraintViolated);
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
