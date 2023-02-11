namespace DentallApp.Features.Reports;

public class ReportQuery : IReportQuery
{
	private readonly AppDbContext _context;
    private readonly IDbConnector _dbConnector;

	public ReportQuery(AppDbContext context, IDbConnector dbConnector)
	{
		_context = context;
        _dbConnector = dbConnector;
    }

    public async Task<ReportGetTotalAppointmentDto> GetTotalAppointmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto)
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
        var result = await connection.QueryAsync<ReportGetTotalAppointmentDto>(sql, new
        {
            AppointmentStatusId.Assisted,
            AppointmentStatusId.NotAssisted,
            AppointmentStatusId.Canceled,
            AppointmentStatusId.Scheduled,
            reportPostDto.From,
            reportPostDto.To,
            reportPostDto.OfficeId,
            reportPostDto.DentistId
        });
        return result.First();
    }

    public async Task<IEnumerable<ReportGetTotalScheduledAppointmentDto>> GetTotalScheduledAppointmentsByDateRangeAsync(ReportPostDto reportPostDto)
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
        return await connection.QueryAsync<ReportGetTotalScheduledAppointmentDto>(sql, new
        {
            AppointmentStatusId.Scheduled,
            reportPostDto.From,
            reportPostDto.To,
            reportPostDto.OfficeId
        });
    }

    public async Task<IEnumerable<ReportGetMostRequestedServicesResponse>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto)
        => await _context.Set<Appointment>()
                         .Include(appointment => appointment.GeneralTreatment)
                         .Where(appointment =>
                               (appointment.AppointmentStatusId == AppointmentStatusId.Assisted) &&
                               (appointment.Date >= reportPostDto.From && appointment.Date <= reportPostDto.To))
                         .OptionalWhere(reportPostDto.OfficeId, appointment => appointment.OfficeId == reportPostDto.OfficeId)
                         .GroupBy(appointment => new { appointment.GeneralTreatmentId, appointment.GeneralTreatment.Name })
                         .Select(group => new ReportGetMostRequestedServicesResponse
                          {
                             DentalServiceName          = group.Key.Name,
                             TotalAppointmentsAssisted   = group.Count()
                          })
                         .OrderByDescending(dto => dto.TotalAppointmentsAssisted)
                         .IgnoreQueryFilters()
                         .ToListAsync();
}
