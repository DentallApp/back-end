namespace DentallApp.Entities.BaseClasses;

public class SoftDeleteEntity : EntityBase
{
    public bool IsDeleted { get; set; }
}
