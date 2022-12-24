namespace DentallApp.Entities;

public class PublicHoliday : SoftDeleteEntity
{
    public string Description { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public List<HolidayOffice> HolidayOffices { get; set; }
}
