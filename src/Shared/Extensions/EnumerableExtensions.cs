namespace DentallApp.Shared.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<int> RemoveDuplicates(this IEnumerable<int> elements)
        => elements.Distinct();
}
