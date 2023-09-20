namespace DentallApp.Domain.Entities;

public class GeneralTreatment : SoftDeleteEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Duration { get; set; }
    public ICollection<SpecificTreatment> SpecificTreatments { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<EmployeeSpecialty> EmployeeSpecialties { get; set; }
}