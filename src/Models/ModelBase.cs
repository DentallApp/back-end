namespace DentallApp.Models;

public class ModelBase
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool State { get; set; } = true;
}