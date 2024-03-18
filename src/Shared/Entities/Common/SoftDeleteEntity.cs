namespace DentallApp.Shared.Entities.Common;

public abstract class SoftDeleteEntity : BaseEntity
{
    public bool IsDeleted { get; set; }

    [Decompile]
    public string GetStatusName()
        => IsDeleted ? StatusType.Inactive: StatusType.Active;

    [Decompile]
    public bool IsActive()
        => !IsDeleted;

    [Decompile]
    public bool IsInactive()
        => IsDeleted;
}
