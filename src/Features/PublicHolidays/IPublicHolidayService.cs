namespace DentallApp.Features.PublicHolidays;

public interface IPublicHolidayService
{
    Task<Response<DtoBase>> CreatePublicHolidayAsync(PublicHolidayInsertDto holidayInsertDto);
    Task<Response> RemovePublicHolidayAsync(int id);
    Task<Response> UpdatePublicHolidayAsync(int id, PublicHolidayUpdateDto holidayUpdateDto);
}
