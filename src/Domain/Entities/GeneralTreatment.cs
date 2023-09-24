namespace DentallApp.Domain.Entities;

public class GeneralTreatment : SoftDeleteEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Duration { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<SpecificTreatment> SpecificTreatments { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<EmployeeSpecialty> EmployeeSpecialties { get; set; }
}