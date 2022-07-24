namespace DentallApp.Models;

public class ModelWithStatus : ModelBase
{
    public int StatusId { get; set; }
    public Status Status { get; set; }
}
