namespace DentallApp.Extensions;

public static class SoftDeleteEntityExtensions
{
    [Decompile]
    public static string GetStatusName(this SoftDeleteEntity entity)
        => entity.IsDeleted ? StatusType.Inactive : StatusType.Active;

    [Decompile]
    public static bool IsActive(this SoftDeleteEntity entity)
        => !entity.IsDeleted;

    [Decompile]
    public static bool IsInactive(this SoftDeleteEntity entity)
        => entity.IsDeleted;
}
