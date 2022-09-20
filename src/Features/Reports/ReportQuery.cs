namespace DentallApp.Features.Reports;

public class ReportQuery : IReportQuery
{
	private readonly AppDbContext _context;

	public ReportQuery(AppDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<ReportGetAppoinmentDto>> GetAppoinmentsByDateRangeAsync(ReportPostWithStatusDto reportPostDto)
	{
		int statusId = reportPostDto.AppoinmentStatusId;
		int officeId = reportPostDto.OfficeId;
		var from	 = reportPostDto.From;
		var to	     = reportPostDto.To;
		IQueryable<Appoinment> queryable;
		var includableQuery = _context.Set<Appoinment>()
									  .Include(appoinment => appoinment.Person)
									  .Include(appoinment => appoinment.GeneralTreatment)
									  .Include(appoinment => appoinment.Employee)
										 .ThenInclude(employee => employee.Person)
									  .Include(appoinment => appoinment.Office)
								      .Include(appoinment => appoinment.AppoinmentStatus);

		queryable = includableQuery.Where(appoinment =>
										 (appoinment.AppoinmentStatusId != AppoinmentStatusId.Scheduled) &&
										 (appoinment.Date >= from && appoinment.Date <= to));

		if (statusId == AppoinmentStatusId.All)
            queryable = queryable.Where(appoinment =>
									   (appoinment.OfficeId == officeId));

        else if (officeId == OfficesId.All)
            queryable = queryable.Where(appoinment =>
                                       (appoinment.AppoinmentStatusId == statusId));

        else
            queryable = queryable.Where(appoinment =>
									   (appoinment.OfficeId == officeId) &&
									   (appoinment.AppoinmentStatusId == statusId));

        return await queryable.OrderBy(appoinment => appoinment.Date)
							  .Select(appoinment => appoinment.MapToReportGetAppoinmentDto())
							  .IgnoreQueryFilters()
							  .ToListAsync();
    }
}
