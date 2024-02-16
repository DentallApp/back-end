namespace DentallApp.Shared.Entities.Common;

public abstract class SoftDeleteEntity : BaseEntity
{
    public bool IsDeleted { get; set; }

    [Decompile]
    public string GetStatusName()
    {
        return IsDeleted ? StatusType.Inactive: StatusType.Active;
    }

    [Decompile]
    public bool IsActive()
    {
        return !IsDeleted;
    }

    [Decompile]
    public bool IsInactive()
    {
        return IsDeleted;
    }
}
