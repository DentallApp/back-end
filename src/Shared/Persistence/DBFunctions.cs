namespace DentallApp.Shared.Persistence;

/// <summary>
/// Provides methods that get translated to database functions when used in LINQ queries.
/// </summary>
public static class DBFunctions
{
    /// <summary>
    /// Adds expr2 to expr1 and returns the result. 
    /// expr1 is a datetime expression, and expr2 is a time expression.
    /// </summary>
    /// <remarks>
    /// Corresponds to MariaDb's <c>ADDTIME(expr1,expr2)</c>.
    /// </remarks>
    public static DateTime AddTime(DateTime expr1, TimeSpan expr2)
        => throw new NotImplementedException();

    /// <summary>
    /// Converts a date to DateTime.
    /// </summary>
    /// <remarks>
    /// Corresponds to MariaDb's <c>CONVERT(expr,datetime)</c>.
    /// </remarks>
    public static DateTime ToDateTime(DateTime expr)
        => throw new NotImplementedException();

    /// <summary>
    /// Returns the difference in days between two date values.
    /// </summary>
    /// <remarks>
    /// Corresponds to MariaDb's <c>DATEDIFF(expr1,expr2)</c>.
    /// </remarks>
    public static int DateDiff(DateTime expr1, DateTime expr2)
        => throw new NotImplementedException();

    /// <summary>
    /// Extracts the date part of the datetime expression <c>expr</c>.
    /// </summary>
    /// <remarks>
    /// Corresponds to MariaDb's <c>DATE(expr)</c>.
    /// </remarks>
    public static DateTime GetDate(DateTime? expr)
        => throw new NotImplementedException();
}
