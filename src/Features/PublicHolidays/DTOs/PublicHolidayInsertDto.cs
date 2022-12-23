namespace DentallApp.Features.PublicHolidays.DTOs;

public class PublicHolidayInsertDto
{
    [Required]
    public string Description { get; set; }
    [Range(1, 31)]
    public int Day { get; set; }
    [Range(1, 12)]
    public int Month { get; set; }
    [Required]
    public List<int> OfficesId { get; set; }
}
