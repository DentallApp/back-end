namespace DentallApp.Features.Reports;

public class ReportGetTotalScheduledAppointmentRequest : IRequest<IEnumerable<ReportGetTotalScheduledAppointmentResponse>>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int OfficeId { get; set; }
}

public class ReportGetTotalScheduledAppointmentsByDateRangeHandler
    : IRequestHandler<ReportGetTotalScheduledAppointmentRequest, IEnumerable<ReportGetTotalScheduledAppointmentResponse>>
{
    private readonly IDbConnector _dbConnector;

    public ReportGetTotalScheduledAppointmentsByDateRangeHandler(IDbConnector dbConnector)
        => _dbConnector = dbConnector;

    public async Task<IEnumerable<ReportGetTotalScheduledAppointmentResponse>> Handle(ReportGetTotalScheduledAppointmentRequest request, CancellationToken cancellationToken)
    {
        using var connection = _dbConnector.CreateConnection();
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
        return await connection.QueryAsync<ReportGetTotalScheduledAppointmentResponse>(sql, new
        {
            AppointmentStatusId.Scheduled,
            request.From,
            request.To,
            request.OfficeId
        });
    }
}
