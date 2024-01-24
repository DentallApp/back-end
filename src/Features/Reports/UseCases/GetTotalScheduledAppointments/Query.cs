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

public class GetTotalScheduledAppointmentsUseCase(IDbConnection dbConnection)
{
    public async Task<IEnumerable<GetTotalScheduledAppointmentsResponse>> ExecuteAsync(GetTotalScheduledAppointmentsRequest request)
    {
        var sql = @"
            SELECT 
            CONCAT(p.names, ' ', p.last_names) AS DentistName,
            o.name AS OfficeName,
            COUNT(*) AS Total
            FROM appointment AS a
            INNER JOIN employee AS e ON e.id = a.dentist_id
            INNER JOIN person AS p ON p.id = e.person_id
            INNER JOIN office AS o ON o.id = a.office_id
            WHERE (a.appointment_status_id = @Scheduled) AND
	              (a.date >= @From AND a.date <= @To) AND
	              (a.office_id = @OfficeId OR @OfficeId = 0)
            GROUP BY a.dentist_id
            ORDER BY Total DESC
        ";
        return await dbConnection.QueryAsync<GetTotalScheduledAppointmentsResponse>(sql, new
        {
            AppointmentStatusId.Scheduled,
            request.From,
            request.To,
            request.OfficeId
        });
    }
}
