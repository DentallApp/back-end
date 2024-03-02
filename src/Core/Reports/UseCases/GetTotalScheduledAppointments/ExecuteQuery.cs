namespace DentallApp.Core.Reports.UseCases.GetTotalScheduledAppointments;

public class GetTotalScheduledAppointmentsRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
}

public class GetTotalScheduledAppointmentsValidator 
    : AbstractValidator<GetTotalScheduledAppointmentsRequest>
{
    public GetTotalScheduledAppointmentsValidator()
    {
        RuleFor(request => request.From).LessThanOrEqualTo(request => request.To);
        RuleFor(request => request.OfficeId).GreaterThanOrEqualTo(0);
    }
}

public class GetTotalScheduledAppointmentsResponse
{
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
    public int Total { get; init; }
}

public class GetTotalScheduledAppointmentsUseCase(
    IDbConnection dbConnection, 
    ISqlCollection sqlCollection,
    GetTotalScheduledAppointmentsValidator validator)
{
    public async Task<ListedResult<GetTotalScheduledAppointmentsResponse>> ExecuteAsync(
        GetTotalScheduledAppointmentsRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        string sql = sqlCollection["GetTotalScheduledAppointments"];
        var appointments = await dbConnection.QueryAsync<GetTotalScheduledAppointmentsResponse>(sql, new
        {
            AppointmentStatus.Predefined.Scheduled,
            request.From,
            request.To,
            request.OfficeId
        });

        return Result.ObtainedResources(appointments);
    }
}
