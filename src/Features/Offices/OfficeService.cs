namespace DentallApp.Features.Offices;

public class OfficeService
{
    private readonly IOfficeRepository _officeRepository;

    public OfficeService(IOfficeRepository officeRepository)
    {
        _officeRepository = officeRepository;
    }

    public async Task<Response<DtoBase>> CreateOfficeAsync(OfficeInsertDto officeInsertDto)
    {
        var office = officeInsertDto.MapToOffice();
        _officeRepository.Insert(office);
        await _officeRepository.SaveAsync();

        return new Response<DtoBase>
        {
            Data    = new DtoBase { Id = office .Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateOfficeAsync(int officeId, int currentEmployeeId, OfficeUpdateDto officeUpdateDto)
    {
        var currentOffice = await _officeRepository.GetOfficeByIdAsync(officeId);
        if (currentOffice is null)
            return new Response(ResourceNotFoundMessage);

        var strategy = _officeRepository.CreateExecutionStrategy();
        await strategy.ExecuteAsync(
            async () =>
            {
                using var transaction = await _officeRepository.BeginTransactionAsync();
                if (officeUpdateDto.IsInactive() && officeUpdateDto.IsCheckboxTicked && currentOffice.IsEnabledEmployeeAccounts)
                    await _officeRepository.DisableEmployeeAccountsAsync(currentEmployeeId, currentOffice);

                else if (officeUpdateDto.IsInactive() && officeUpdateDto.IsCheckboxUnticked && currentOffice.IsDisabledEmployeeAccounts)
                    await _officeRepository.EnableEmployeeAccountsAsync(currentOffice);

                else if (officeUpdateDto.IsActive() && currentOffice.IsDisabledEmployeeAccounts)
                    await _officeRepository.EnableEmployeeAccountsAsync(currentOffice);

                officeUpdateDto.MapToOffice(currentOffice);
                await _officeRepository.SaveAsync();
                await transaction.CommitAsync();
            });

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }
}
