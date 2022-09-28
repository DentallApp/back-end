namespace DentallApp.Features.Appoinments;

public class AppoinmentService : IAppoinmentService
{
    private readonly IAppoinmentRepository _appoinmentRepository;
    private readonly IInstantMessaging _instantMessaging;

    public AppoinmentService(IAppoinmentRepository appoinmentRepository, IInstantMessaging instantMessaging)
    {
        _appoinmentRepository = appoinmentRepository;
        _instantMessaging = instantMessaging;
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

    public async Task<Response> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppoinmentCancelDto appoinmentCancelDto)
    {
        try
        {
            var appoinmentsId = appoinmentCancelDto.Appoinments.Select(appoinment => appoinment.AppoinmentId);
            var businessName = EnvReader.Instance[AppSettings.BusinessName];
            if (currentEmployee.IsOnlyDentist())
                await _appoinmentRepository.CancelAppointmentsByDentistIdAsync(currentEmployee.GetEmployeeId(), appoinmentsId);
            else
                await _appoinmentRepository.CancelAppointmentsByOfficeIdAsync(
                        currentEmployee.IsSuperAdmin() ? default : currentEmployee.GetOfficeId(), 
                        appoinmentsId
                    );

            foreach (var appoinment in appoinmentCancelDto.Appoinments)
            {
                var msg = string.Format("Estimado usuario {0}, su cita agendada en el consultorio odontológico {1} para el día {2} a las {3} ha sido cancelada por el siguiente motivo: {4}",
                                           appoinment.PatientName, 
                                           businessName, 
                                           appoinment.AppoinmentDate,
                                           appoinment.StartHour, 
                                           appoinmentCancelDto.Reason
                                       );
                await _instantMessaging.SendMessageAsync(appoinment.PatientCellPhone, msg);
            }
        }
        catch(Exception ex)
        {
            return new Response(ex.Message);
        }

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response<IEnumerable<AppoinmentGetByEmployeeDto>>> GetAppoinmentsForEmployeeAsync(ClaimsPrincipal currentEmployee, AppoinmentPostDateDto appoinmentPostDto)
    {
        if (currentEmployee.IsOnlyDentist() && currentEmployee.GetEmployeeId() != appoinmentPostDto.DentistId)
            return new Response<IEnumerable<AppoinmentGetByEmployeeDto>>(CanOnlyAccessYourOwnAppoinmentsMessage);

        if (!currentEmployee.IsSuperAdmin() && currentEmployee.IsNotInOffice(appoinmentPostDto.OfficeId))
            return new Response<IEnumerable<AppoinmentGetByEmployeeDto>>(OfficeNotAssignedMessage);

        var data = await _appoinmentRepository.GetAppoinmentsForEmployeeAsync(appoinmentPostDto);
        return new Response<IEnumerable<AppoinmentGetByEmployeeDto>>
        {
            Success = true,
            Message = GetResourceMessage,
            Data = data
        };
    }
}
