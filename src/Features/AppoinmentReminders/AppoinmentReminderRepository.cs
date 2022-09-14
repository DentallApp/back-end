using LinqToDB;
using LinqToDB.EntityFrameworkCore;

namespace DentallApp.Features.AppoinmentReminders;

public class AppoinmentReminderRepository : IAppoinmentReminderRepository
{
    private readonly AppDbContext _context;

    static AppoinmentReminderRepository()
    {
        LinqToDBForEFTools.Initialize();
    }

    public AppoinmentReminderRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<AppoinmentReminderDto> GetScheduledAppoinments(int timeInAdvance, DateTime currentDate)
        => _context.Set<Appoinment>()
                   .Include(appoinment => appoinment.Person)
                   .Include(appoinment => appoinment.Employee)
                      .ThenInclude(employee => employee.Person)
                   .Where(appoinment =>
                          appoinment.AppoinmentStatusId == AppoinmentStatusId.Scheduled &&
                          appoinment.HasNotReminder() &&
                          _context.DateDiff(appoinment.Date, currentDate) == timeInAdvance)
                   .Select(appoinment => new AppoinmentReminderDto
                    {
                        AppoinmentId     = appoinment.Id,
                        PatientName      = appoinment.Person.FullName,
                        PatientCellPhone = appoinment.Person.CellPhone,
                        Date             = appoinment.Date,
                        StartHour        = appoinment.StartHour,
                        DentistName      = appoinment.Employee.Person.FullName
                    })
                   .IgnoreQueryFilters()
                   .ToList();

    public void UpdateScheduledAppoinments(IEnumerable<int> appoinmentsId)
        => _context.Set<Appoinment>()
                   .Where(appoinment => appoinmentsId.Contains(appoinment.Id))
                   .Set(appoinment => appoinment.HasReminder, true)
                   .Set(appoinment => appoinment.UpdatedAt, DateTime.Now)
                   .Update();
}
