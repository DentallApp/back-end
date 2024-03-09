namespace DentallApp.Core.Reports.UseCases.GetTotalAppointments;

public class GetTotalAppointmentsRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
    public int DentistId { get; init; }
}

public class GetTotalAppointmentsValidator : AbstractValidator<GetTotalAppointmentsRequest>
{
    public GetTotalAppointmentsValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.OfficeId).GreaterThanOrEqualTo(0);
        RuleFor(request => request.DentistId).GreaterThanOrEqualTo(0);
    }
}

public class GetTotalAppointmentsResponse
{
    public int Total { get; init; }
    public int TotalAppointmentsAssisted { get; init; }
    public int TotalAppointmentsNotAssisted { get; init; }
    public int TotalAppointmentsCancelledByPatient { get; init; }
    public int TotalAppointmentsCancelledByEmployee { get; init; }
}

public class GetTotalAppointmentsUseCase(
    IDbConnection dbConnection, 
    ISqlCollection sqlCollection,
    ICurrentEmployee currentEmployee,
    GetTotalAppointmentsValidator validator)
{
    public async Task<Result<GetTotalAppointmentsResponse>> ExecuteAsync(GetTotalAppointmentsRequest request)
    {
        if (currentEmployee.IsAdmin() && currentEmployee.IsNotInOffice(request.OfficeId))
            return Result.Forbidden();

        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        string sql = sqlCollection["GetTotalAppointments"];
        var totalAppointments = await dbConnection.QueryFirstAsync<GetTotalAppointmentsResponse>(sql, new
        {
            AppointmentStatus.Predefined.Assisted,
            AppointmentStatus.Predefined.NotAssisted,
            AppointmentStatus.Predefined.Canceled,
            AppointmentStatus.Predefined.Scheduled,
            request.From,
            request.To,
            request.OfficeId,
            request.DentistId
        });

        return Result.ObtainedResource(totalAppointments);
    }
}
