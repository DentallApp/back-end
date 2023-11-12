namespace DentallApp.Shared.Extensions;

public static class ResultTypeExtensions
{
    public static Result<T> WithData<T>(this Result result, T data)
        => result.ToResult(data);
}
