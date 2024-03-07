namespace DentallApp.HostApplication.ExceptionHandlers;

public class ReferenceConstraintExceptionHandler(
    ILogger<ReferenceConstraintExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        if (exception is not ReferenceConstraintException)
            return false;

        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
        Result result = Result.Invalid(Messages.ReferenceConstraintViolated);
        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
