namespace DentallApp.Extensions;

public static class ModelWithSoftDeleteExtensions
{
    [Decompile]
    public static string GetStatusName(this ModelWithSoftDelete model)
        => model.IsDeleted ? StatusType.Inactive : StatusType.Active;

    [Decompile]
    public static bool IsActive(this ModelWithSoftDelete model)
        => !model.IsDeleted;

    [Decompile]
    public static bool IsInactive(this ModelWithSoftDelete model)
        => model.IsDeleted;
}
