namespace DentallApp.Features.Reports.UseCases.GetTotalAppointments;

public class GetTotalAppointmentsRequest
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public int OfficeId { get; init; }
    public int DentistId { get; init; }
}

public class GetTotalAppointmentsResponse
{
    public int Total { get; init; }
    public int TotalAppointmentsAssisted { get; init; }
    public int TotalAppointmentsNotAssisted { get; init; }
    public int TotalAppointmentsCancelledByPatient { get; init; }
    public int TotalAppointmentsCancelledByEmployee { get; init; }
}

public class GetTotalAppointmentsUseCase(IDbConnection dbConnection, ISqlCollection sqlCollection)
{
    public async Task<GetTotalAppointmentsResponse> ExecuteAsync(GetTotalAppointmentsRequest request)
    {
        string sql = sqlCollection["GetTotalAppointments"];
        var result = await dbConnection.QueryAsync<GetTotalAppointmentsResponse>(sql, new
        {
            AppointmentStatusId.Assisted,
            AppointmentStatusId.NotAssisted,
            AppointmentStatusId.Canceled,
            AppointmentStatusId.Scheduled,
            request.From,
            request.To,
            request.OfficeId,
            request.DentistId
        });
        return result.First();
    }
}
