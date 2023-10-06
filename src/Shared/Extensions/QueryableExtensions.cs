namespace DentallApp.Shared.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> OptionalWhere<T, U>(
        this IQueryable<T> source,
        U applyFilter,
        Expression<Func<T, bool>> predicate) where U : class
    {
        return applyFilter is not null ? source.Where(predicate) : source;
    }

    public static IQueryable<T> OptionalWhere<T, U>(
        this IQueryable<T> source,
        U? applyFilter,
        Expression<Func<T, bool>> predicate) where U : struct
    {
        return applyFilter.HasValue ? source.Where(predicate) : source;
    }

    public static IQueryable<T> OptionalWhere<T>(
        this IQueryable<T> source,
        int applyFilter,
        Expression<Func<T, bool>> predicate)
    { 
        return applyFilter == default ? source : source.Where(predicate);
    }
}
