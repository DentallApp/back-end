namespace DentallApp.Features.OfficeSchedules;

public class OfficeScheduleRepository : SoftDeleteRepository<OfficeSchedule>, IOfficeScheduleRepository
{
    public OfficeScheduleRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<OfficeScheduleGetAllDto>> GetAllOfficeSchedulesAsync()
        => await Context.Set<Office>()
                        .Include(office => office.OfficeSchedules)
                           .ThenInclude(officeSchedule => officeSchedule.WeekDay)
                        .Where(office => office.OfficeSchedules.Any())
                        .Select(office => office.MapToOfficeScheduleGetAllDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();

    public async Task<OfficeSchedule> GetOfficeScheduleByIdAsync(int id)
        => await Context.Set<OfficeSchedule>()
                        .Where(officeSchedule => officeSchedule.Id == id)
                        .IgnoreQueryFilters()
                        .FirstOrDefaultAsync();

    public async Task<IEnumerable<OfficeScheduleGetDto>> GetOfficeScheduleByOfficeIdAsync(int officeId)
        => await Context.Set<OfficeSchedule>()
                        .Include(officeSchedule => officeSchedule.WeekDay)
                        .Where(officeSchedule => officeSchedule.OfficeId == officeId)
                        .OrderBy(officeSchedule => officeSchedule.WeekDayId)
                        .Select(officeSchedule => officeSchedule.MapToOfficeScheduleGetDto())
                        .IgnoreQueryFilters()
                        .ToListAsync();
}
