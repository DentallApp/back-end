namespace DentallApp.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<int> RemoveDuplicates(this IEnumerable<int> elements)
        => elements.GroupBy(item => item).Select(group => group.Key);
}
