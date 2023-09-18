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

public class GetTotalAppointmentsUseCase
{
    private readonly IDbConnection _dbConnection;

    public GetTotalAppointmentsUseCase(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<GetTotalAppointmentsResponse> Execute(GetTotalAppointmentsRequest request)
    {
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
        var result = await _dbConnection.QueryAsync<GetTotalAppointmentsResponse>(sql, new
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
