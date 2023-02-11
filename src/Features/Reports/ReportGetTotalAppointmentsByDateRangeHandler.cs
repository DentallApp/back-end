namespace DentallApp.Features.Reports;

public class ReportGetTotalAppointmentRequest : IRequest<ReportGetTotalAppointmentResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int OfficeId { get; set; }
    public int DentistId { get; set; }
}

public class ReportGetTotalAppointmentsByDateRangeHandler
    : IRequestHandler<ReportGetTotalAppointmentRequest, ReportGetTotalAppointmentResponse>
{
    private readonly IDbConnector _dbConnector;

    public ReportGetTotalAppointmentsByDateRangeHandler(IDbConnector dbConnector)
        => _dbConnector = dbConnector;

    public async Task<ReportGetTotalAppointmentResponse> Handle(ReportGetTotalAppointmentRequest request, CancellationToken cancellationToken)
    {
        using var connection = _dbConnector.CreateConnection();
        var sql = @"
            SELECT 
            COUNT(*) AS Total,
            SUM(case when appointment_status_id = @Assisted then 1 else 0 END) 
	            AS TotalAppointmentsAssisted,
            SUM(case when appointment_status_id = @NotAssisted then 1 else 0 END) 
	            AS TotalAppointmentsNotAssisted,
            SUM(case when appointment_status_id = @Canceled AND is_cancelled_by_employee = 1 then 1 else 0 END) 
	            AS TotalAppointmentsCancelledByEmployee,
            SUM(case when appointment_status_id = @Canceled AND is_cancelled_by_employee = 0 then 1 else 0 END) 
	            AS TotalAppointmentsCancelledByPatient
            FROM appointment AS a
            WHERE (a.appointment_status_id <> @Scheduled) AND
                  (a.date >= @From AND a.date <= @To) AND
                  (a.office_id = @OfficeId OR @OfficeId = 0) AND
                  (a.dentist_id = @DentistId OR @DentistId = 0)
        ";
        var result = await connection.QueryAsync<ReportGetTotalAppointmentResponse>(sql, new
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
