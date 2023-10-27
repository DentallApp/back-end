namespace DentallApp.Features.Appointments.UseCases;

public class GetAppointmentsByDateRangeRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
    public int DentistId { get; init; }
    public int StatusId { get; init; }
}

public class GetAppointmentsByDateRangeResponse
{
    public int AppointmentId { get; init; }
    public string PatientName { get; init; }
    public string CreatedAt { get; init; }
    public string AppointmentDate { get; init; }
    public string StartHour { get; init; }
    public string EndHour { get; init; }
    public string DentalServiceName { get; init; }
    public string Document { get; init; }
    public string CellPhone { get; init; }
    public string Email { get; init; }
    public DateTime? DateBirth { get; init; }
    public string Status { get; init; }
    public int StatusId { get; init; }
    public int DentistId { get; init; }
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
}

public class GetAppointmentsByDateRangeUseCase
{
    private readonly DbContext _context;

    public GetAppointmentsByDateRangeUseCase(DbContext context)
    {
        _context = context;
    }

    public async Task<ListedResult<GetAppointmentsByDateRangeResponse>> ExecuteAsync(
        ClaimsPrincipal currentEmployee, 
        GetAppointmentsByDateRangeRequest request)
    {
        if (currentEmployee.IsOnlyDentist() && currentEmployee.GetEmployeeId() != request.DentistId)
            return Result.Forbidden(CanOnlyAccessYourOwnAppointmentsMessage);

        if (!currentEmployee.IsSuperAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden(OfficeNotAssignedMessage);

        var appointments = await _context.Set<Appointment>()
            .OptionalWhere(request.StatusId, appointment => appointment.AppointmentStatusId == request.StatusId)
            .OptionalWhere(request.OfficeId, appointment => appointment.OfficeId == request.OfficeId)
            .OptionalWhere(request.DentistId, appointment => appointment.DentistId == request.DentistId)
            .Where(appointment => appointment.Date >= request.From && appointment.Date <= request.To)
            .OrderBy(appointment => appointment.Date)
            .Select(appointment => new GetAppointmentsByDateRangeResponse
            {
                AppointmentId      = appointment.Id,
                PatientName        = appointment.Person.FullName,
                CreatedAt          = appointment.CreatedAt.GetDateAndHourInSpanishFormat(),
                AppointmentDate    = appointment.Date.GetDateWithStandardFormat(),
                StartHour          = appointment.StartHour.GetHourWithoutSeconds(),
                EndHour            = appointment.EndHour.GetHourWithoutSeconds(),
                DentalServiceName  = appointment.GeneralTreatment.Name,
                Document           = appointment.Person.Document,
                CellPhone          = appointment.Person.CellPhone,
                Email              = appointment.Person.Email,
                DateBirth          = appointment.Person.DateBirth,
                DentistId          = appointment.DentistId,
                DentistName        = appointment.Employee.Person.FullName,
                Status             = appointment.AppointmentStatus.Name,
                StatusId           = appointment.AppointmentStatusId,
                OfficeName         = appointment.Office.Name
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .ToListAsync();

        return Result.Success(appointments);
    }
}
