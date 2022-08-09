namespace DentallApp.Models;

public class ModelWithSoftDelete : ModelBase
{
    public bool IsDeleted { get; set; }
}
