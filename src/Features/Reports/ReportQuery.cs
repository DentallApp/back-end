using Dapper;
using MySqlConnector;

namespace DentallApp.Features.Reports;

public class ReportQuery : IReportQuery
{
	private readonly AppDbContext _context;
    private readonly AppSettings _settings;

	public ReportQuery(AppDbContext context, AppSettings settings)
	{
		_context = context;
        _settings = settings;
    }

    public async Task<ReportGetTotalAppoinmentDto> GetTotalAppoinmentsByDateRangeAsync(ReportPostWithDentistDto reportPostDto)
    {
        using var connection = new MySqlConnection(_settings.ConnectionString);
        var sql = @"
            SELECT 
            COUNT(*) AS Total,
            SUM(case when appoinment_status_id = @Assisted then 1 else 0 END) 
	            AS TotalAppoinmentsAssisted,
            SUM(case when appoinment_status_id = @NotAssisted then 1 else 0 END) 
	            AS TotalAppoinmentsNotAssisted,
            SUM(case when appoinment_status_id = @Canceled AND is_cancelled_by_employee = 1 then 1 else 0 END) 
	            AS TotalAppoinmentsCancelledByEmployee,
            SUM(case when appoinment_status_id = @Canceled AND is_cancelled_by_employee = 0 then 1 else 0 END) 
	            AS TotalAppoinmentsCancelledByPatient
            FROM appoinments AS a
            WHERE (a.appoinment_status_id <> @Scheduled) AND
                  (a.date >= @From AND a.date <= @To) AND
                  (a.office_id = @OfficeId OR @OfficeId = 0) AND
                  (a.dentist_id = @DentistId OR @DentistId = 0)
        ";
        var result = await connection.QueryAsync<ReportGetTotalAppoinmentDto>(sql, new
        {
            AppoinmentStatusId.Assisted,
            AppoinmentStatusId.NotAssisted,
            AppoinmentStatusId.Canceled,
            AppoinmentStatusId.Scheduled,
            reportPostDto.From,
            reportPostDto.To,
            reportPostDto.OfficeId,
            reportPostDto.DentistId
        });
        return result.First();
    }

    public async Task<IEnumerable<ReportGetTotalScheduledAppoinmentDto>> GetTotalScheduledAppoinmentsByDateRangeAsync(ReportPostDto reportPostDto)
    {
        using var connection = new MySqlConnection(_settings.ConnectionString);
        var sql = @"
            SELECT 
            CONCAT(p.names, ' ', p.last_names) AS DentistName,
            o.name AS OfficeName,
            COUNT(*) AS Total
            FROM appoinments AS a
            INNER JOIN employees AS e ON e.id = a.dentist_id
            INNER JOIN persons AS p ON p.id = e.person_id
            INNER JOIN offices AS o ON o.id = a.office_id
            WHERE (a.appoinment_status_id = @Scheduled) AND
	              (a.date >= @From AND a.date <= @To) AND
	              (a.office_id = @OfficeId OR @OfficeId = 0)
            GROUP BY a.dentist_id
            ORDER BY Total DESC
        ";
        return await connection.QueryAsync<ReportGetTotalScheduledAppoinmentDto>(sql, new
        {
            AppoinmentStatusId.Scheduled,
            reportPostDto.From,
            reportPostDto.To,
            reportPostDto.OfficeId
        });
    }

    public async Task<IEnumerable<ReportGetMostRequestedServicesDto>> GetMostRequestedServicesAsync(ReportPostDto reportPostDto)
        => await _context.Set<Appoinment>()
                         .Include(appoinment => appoinment.GeneralTreatment)
                         .Where(appoinment =>
                               (appoinment.AppoinmentStatusId == AppoinmentStatusId.Assisted) &&
                               (appoinment.Date >= reportPostDto.From && appoinment.Date <= reportPostDto.To))
                         .OptionalWhere(reportPostDto.OfficeId, appoinment => appoinment.OfficeId == reportPostDto.OfficeId)
                         .GroupBy(appoinment => new { appoinment.GeneralTreatmentId, appoinment.GeneralTreatment.Name })
                         .Select(group => new ReportGetMostRequestedServicesDto
                          {
                             DentalServiceName          = group.Key.Name,
                             TotalAppoinmentsAssisted   = group.Count()
                          })
                         .OrderByDescending(dto => dto.TotalAppoinmentsAssisted)
                         .IgnoreQueryFilters()
                         .ToListAsync();
}
