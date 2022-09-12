namespace DentallApp.Features.Appoinments;

public class AppoinmentService : IAppoinmentService
{
    private readonly IAppoinmentRepository _appoinmentRepository;

    public AppoinmentService(IAppoinmentRepository appoinmentRepository)
    {
        _appoinmentRepository = appoinmentRepository;
    }

    public async Task<IEnumerable<AppoinmentGetByBasicUserDto>> GetAppoinmentsByUserIdAsync(int userId)
        => await _appoinmentRepository.GetAppoinmentsByUserIdAsync(userId);

    public async Task<Response> CancelBasicUserAppointmentAsync(int appoinmentId, int currentUserId)
    {
        var appoinment = await _appoinmentRepository.GetByIdAsync(appoinmentId);
        if (appoinment is null)
            return new Response(ResourceNotFoundMessage);

        if (appoinment.UserId != currentUserId)
            return new Response(AppoinmentNotAssignedMessage);

        appoinment.AppoinmentStatusId = AppoinmentStatusId.Canceled;
        await _appoinmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }

    public async Task<Response> CreateAppoinmentAsync(AppoinmentInsertDto appoinmentInsertDto)
    {
        if (await _appoinmentRepository.IsNotAvailableAsync(appoinmentInsertDto))
            return new Response(DateAndTimeAppointmentIsNotAvailableMessage);

        _appoinmentRepository.Insert(appoinmentInsertDto.MapToAppoinment());
        await _appoinmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateAppoinmentAsync(int id, ClaimsPrincipal currentEmployee, AppoinmentUpdateDto appoinmentUpdateDto)
    {
        var appoinment = await _appoinmentRepository.GetByIdAsync(id);
        if (appoinment is null)
            return new Response(ResourceNotFoundMessage);

        if (appoinment.AppoinmentStatusId == AppoinmentStatusId.Canceled)
            return new Response(AppoinmentIsAlreadyCanceledMessage);

        if (currentEmployee.IsOnlyDentist() && appoinment.DentistId != currentEmployee.GetEmployeeId())
            return new Response(AppoinmentNotAssignedMessage);

        if (currentEmployee.IsNotInOffice(appoinment.OfficeId))
            return new Response(OfficeNotAssignedMessage);

        appoinmentUpdateDto.MapToAppoinment(appoinment);
        await _appoinmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> CancelAppointmentsByOfficeIdAsync(int officeId, AppoinmentCancelByEmployeeDto appoinmentCancelDto)
    {
        var appoinmentsId = appoinmentCancelDto.Appoinments.Select(appoinment => appoinment.AppoinmentId);
        await _appoinmentRepository.CancelOfficeAppointmentsAsync(officeId, appoinmentsId);

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> CancelAppointmentsByDentistIdAsync(int dentistId, AppoinmentCancelByDentistDto appoinmentCancelDto)
    {
        var appoinmentsId = appoinmentCancelDto.Appoinments.Select(appoinment => appoinment.AppoinmentId);
        await _appoinmentRepository.CancelDentistAppointmentsAsync(dentistId, appoinmentsId);

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<IEnumerable<AppoinmentGetByEmployeeDto>> GetAppointmentsByOfficeIdAsync(int officeId, AppoinmentPostDateDto appoinmentDto)
        => await _appoinmentRepository.GetAppointmentsByOfficeIdAsync(officeId, appoinmentDto.From, appoinmentDto.To);

    public async Task<IEnumerable<AppoinmentGetByDentistDto>> GetAppointmentsByDentistIdAsync(int dentistId, AppoinmentPostDateDto appoinmentDto)
        => await _appoinmentRepository.GetAppointmentsByDentistIdAsync(dentistId, appoinmentDto.From, appoinmentDto.To);

    public async Task<IEnumerable<AppoinmentScheduledGetByEmployeeDto>> GetScheduledAppointmentsByOfficeIdAsync(int officeId, AppoinmentPostDateDto appoinmentDto)
        => await _appoinmentRepository.GetScheduledAppointmentsByOfficeIdAsync(officeId, appoinmentDto.From, appoinmentDto.To);

    public async Task<IEnumerable<AppoinmentScheduledGetByDentistDto>> GetScheduledAppointmentsByDentistIdAsync(int dentistId, AppoinmentPostDateDto appoinmentDto)
        => await _appoinmentRepository.GetScheduledAppointmentsByDentistIdAsync(dentistId, appoinmentDto.From, appoinmentDto.To);
}
