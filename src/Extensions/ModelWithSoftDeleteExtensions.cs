namespace DentallApp.Extensions;

public static class ModelWithSoftDeleteExtensions
{
    [Decompile]
    public static string GetStatusName(this ModelWithSoftDelete model)
        => model.IsDeleted ? StatusType.Inactive : StatusType.Active;
}
