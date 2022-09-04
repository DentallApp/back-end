namespace DentallApp.Features.Offices;

public class OfficeService : IOfficeService
{
    private readonly IOfficeRepository _officeRepository;

    public OfficeService(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Response> CreateOfficeAsync(OfficeInsertDto officeInsertDto)
    {
        _officeRepository.Insert(officeInsertDto.MapToOfficeDto());
        await _officeRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateOfficeAsync(int officeId, int currentEmployeeId, OfficeUpdateDto officeUpdateDto)
    {
        var currentOffice = await _officeRepository.GetOfficeByIdAsync(officeId);
        if (currentOffice is null)
            return new Response(ResourceNotFoundMessage);

        using var transaction = await _officeRepository.BeginTransactionAsync();
        
        if(officeUpdateDto.IsInactive() && officeUpdateDto.IsCheckboxTicked && currentOffice.IsEnabledEmployeeAccounts)
            await _officeRepository.DisableEmployeeAccountsAsync(currentEmployeeId, currentOffice);

        else if(officeUpdateDto.IsInactive() && officeUpdateDto.IsCheckboxUnticked && currentOffice.IsDisabledEmployeeAccounts)
            await _officeRepository.EnableEmployeeAccountsAsync(currentOffice);

        else if(officeUpdateDto.IsActive() && currentOffice.IsDisabledEmployeeAccounts)
            await _officeRepository.EnableEmployeeAccountsAsync(currentOffice);

        officeUpdateDto.MapToOfficeDto(currentOffice);
        await _officeRepository.SaveAsync();
        await transaction.CommitAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<IEnumerable<OfficeGetDto>> GetOfficesAsync()
        => await _officeRepository.GetOfficesAsync();

    public async Task<IEnumerable<OfficeShowDto>> GetOfficesForEditAsync()
        => await _officeRepository.GetOfficesForEditAsync();

    public async Task<IEnumerable<OfficeGetDto>> GetAllOfficesAsync()
        => await _officeRepository.GetAllOfficesAsync();
}
