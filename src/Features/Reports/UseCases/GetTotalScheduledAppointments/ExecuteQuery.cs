namespace DentallApp.Features.Reports.UseCases.GetTotalScheduledAppointments;

public class GetTotalScheduledAppointmentsRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
}

public class GetTotalScheduledAppointmentsResponse
{
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
    public int Total { get; init; }
}

public class GetTotalScheduledAppointmentsUseCase(IDbConnection dbConnection, ISqlCollection sqlCollection)
{
    public async Task<IEnumerable<GetTotalScheduledAppointmentsResponse>> ExecuteAsync(GetTotalScheduledAppointmentsRequest request)
    {
        string sql = sqlCollection["GetTotalScheduledAppointments"];
        return await dbConnection.QueryAsync<GetTotalScheduledAppointmentsResponse>(sql, new
        {
            AppointmentStatusId.Scheduled,
            request.From,
            request.To,
            request.OfficeId
        });
    }
}
