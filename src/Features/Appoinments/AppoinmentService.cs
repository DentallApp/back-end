namespace DentallApp.Features.Appoinments;

public partial class AppoinmentService : IAppoinmentService
{
    private readonly IAppoinmentRepository _appoinmentRepository;
    private readonly IInstantMessaging _instantMessaging;
    private readonly ISpecificTreatmentRepository _treatmentRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AppoinmentService(IAppoinmentRepository appoinmentRepository, 
                             IInstantMessaging instantMessaging,
                             ISpecificTreatmentRepository treatmentRepository,
                             IDateTimeProvider dateTimeProvider)
    {
        _appoinmentRepository = appoinmentRepository;
        _instantMessaging = instantMessaging;
        _treatmentRepository = treatmentRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Response> CancelBasicUserAppointmentAsync(int appoinmentId, int currentUserId)
    {
        var appoinment = await _appoinmentRepository.GetByIdAsync(appoinmentId);
        if (appoinment is null)
            return new Response(ResourceNotFoundMessage);

        if (appoinment.UserId != currentUserId)
            return new Response(AppoinmentNotAssignedMessage);

        if (_dateTimeProvider.Now > (appoinment.Date + appoinment.StartHour))
            return new Response(AppoinmentThatHasAlreadyPassedBasicUserMessage);

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

        var appoinment = appoinmentInsertDto.MapToAppoinment();
        _appoinmentRepository.Insert(appoinment);
        await _appoinmentRepository.SaveAsync();
        await SendAppoinmentInformationAsync(appoinment.Id, appoinmentInsertDto);
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

        if (_dateTimeProvider.Now.Date > appoinment.Date)
            return new Response(AppoinmentCannotBeUpdatedForPreviousDaysMessage);

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

    public async Task<Response<AppoinmentsThatCannotBeCanceledDto>> CancelAppointmentsAsync(ClaimsPrincipal currentEmployee, AppoinmentCancelDto appoinmentCancelDto)
    {
        // Almacena las citas cuya fecha y hora estipulada NO hayan pasado.
        var appointmentsCanBeCancelled = appoinmentCancelDto.Appoinments
                                                       .Where(appoinmentDto => 
                                                             (appoinmentDto.AppoinmentDate + appoinmentDto.StartHour) > _dateTimeProvider.Now);
        var appointmentsIdCanBeCancelled = appointmentsCanBeCancelled.Select(appoinmentDto => appoinmentDto.AppoinmentId);
        try
        {
            if (currentEmployee.IsOnlyDentist())
                await _appoinmentRepository.CancelAppointmentsByDentistIdAsync(currentEmployee.GetEmployeeId(), appointmentsIdCanBeCancelled);
            else
                await _appoinmentRepository.CancelAppointmentsByOfficeIdAsync(
                        currentEmployee.IsSuperAdmin() ? default : currentEmployee.GetOfficeId(),
                        appointmentsIdCanBeCancelled
                    );
            await SendMessageAboutAppoinmentCancellationAsync(appointmentsCanBeCancelled, appoinmentCancelDto.Reason);
        }
        catch(Exception ex)
        {
            return new Response<AppoinmentsThatCannotBeCanceledDto>(ex.Message);
        }

        if(appoinmentCancelDto.Appoinments.Count() != appointmentsCanBeCancelled.Count())
        {
            int count   = appoinmentCancelDto.Appoinments.Count() - appointmentsCanBeCancelled.Count();
            var message = string.Format(AppoinmentThatHasAlreadyPassedEmployeeMessage, count);
            var data    = new AppoinmentsThatCannotBeCanceledDto
            {
                AppoinmentsId = appoinmentCancelDto.Appoinments
                                                   .Select(appoinmentDto => appoinmentDto.AppoinmentId)
                                                   .Except(appointmentsIdCanBeCancelled)
            };
            return new Response<AppoinmentsThatCannotBeCanceledDto> { Message = message, Data = data };
        }

        return new Response<AppoinmentsThatCannotBeCanceledDto>
        {
            Success = true,
            Message = SuccessfullyCancelledAppointmentsMessage
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
