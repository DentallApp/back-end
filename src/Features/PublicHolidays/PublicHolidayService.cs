namespace DentallApp.Features.PublicHolidays;

public class PublicHolidayService : IPublicHolidayService
{
    private readonly IUnitOfWork _unitOfWork;

    public PublicHolidayService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<DtoBase>> CreatePublicHolidayAsync(PublicHolidayInsertDto holidayInsertDto)
    {
        var publicHoliday = holidayInsertDto.MapToPublicHoliday();
        _unitOfWork.PublicHolidayRepository.Insert(publicHoliday);
        foreach(var officeId in holidayInsertDto.OfficesId.RemoveDuplicates())
        {
            var holidayOffice = new HolidayOffice { OfficeId = officeId };
            _unitOfWork.HolidayOfficeRepository.Insert(holidayOffice);
            holidayOffice.PublicHoliday = publicHoliday;
        }
        await _unitOfWork.SaveChangesAsync();

        return new Response<DtoBase>
        {
            Data    = new DtoBase { Id = publicHoliday.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> RemovePublicHolidayAsync(int id)
    {
        var holiday = await _unitOfWork.PublicHolidayRepository.GetByIdAsync(id);
        if (holiday is null)
            return new Response(ResourceNotFoundMessage);

        _unitOfWork.PublicHolidayRepository.SoftDelete(holiday);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> UpdatePublicHolidayAsync(int id, PublicHolidayUpdateDto holidayUpdateDto)
    {
        var holiday = await _unitOfWork.PublicHolidayRepository.GetPublicHolidayAsync(id);
        if (holiday is null)
            return new Response(ResourceNotFoundMessage);

        holidayUpdateDto.MapToPublicHoliday(holiday);
        _unitOfWork.HolidayOfficeRepository.UpdateHolidayOffices(holiday.Id, holiday.HolidayOffices, holidayUpdateDto.OfficesId);
        await _unitOfWork.SaveChangesAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
