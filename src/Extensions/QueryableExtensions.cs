namespace DentallApp.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OptionalWhere<T>(this IQueryable<T> source, string optionalParam, Expression<Func<T, bool>> predicate)
        => optionalParam == default ? source : source.Where(predicate);

    public static IQueryable<T> OptionalWhere<T>(this IQueryable<T> source, int optionalParam, Expression<Func<T, bool>> predicate) 
        => optionalParam == default ? source : source.Where(predicate);
}
