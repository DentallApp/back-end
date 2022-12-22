namespace DentallApp.Entities;

public class OfficeHoliday : EntityBase
{
    public int PublicHolidayId { get; set; }
    public PublicHoliday PublicHoliday { get; set; }
    public int OfficeId { get; set; }
    public Office Office { get; set; }
}
