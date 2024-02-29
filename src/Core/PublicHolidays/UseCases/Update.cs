namespace DentallApp.Core.PublicHolidays.UseCases;

public class UpdatePublicHolidayRequest
{
    public string Description { get; init; }
    public int Day { get; init; }
    public int Month { get; init; }
    public List<int> OfficesId { get; init; }

    public void MapToPublicHoliday(PublicHoliday publicHoliday)
    {
        publicHoliday.Description = Description;
        publicHoliday.Day         = Day;
        publicHoliday.Month       = Month;
    }
}

public class UpdatePublicHolidayValidator : AbstractValidator<UpdatePublicHolidayRequest>
{
    public UpdatePublicHolidayValidator()
    {
        RuleFor(request => request.Description).NotEmpty();
        RuleFor(request => request.Day).InclusiveBetween(1, 31);
        RuleFor(request => request.Month).InclusiveBetween(1, 12);
        RuleFor(request => request.OfficesId).NotEmpty();
        RuleForEach(request => request.OfficesId).GreaterThan(0);
    }
}

public class UpdatePublicHolidayUseCase(
    DbContext context,
    IEntityService<OfficeHoliday> officeHolidayService,
    UpdatePublicHolidayValidator validator)
{
    public async Task<Result> ExecuteAsync(int id, UpdatePublicHolidayRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var holiday = await context.Set<PublicHoliday>()
            .Include(publicHoliday => publicHoliday.Offices)
            .Where(publicHoliday => publicHoliday.Id == id)
            .FirstOrDefaultAsync();

        if (holiday is null)
            return Result.NotFound();

        request.MapToPublicHoliday(holiday);
        AssignOfficesToHoliday(holiday.Id, holiday.Offices, request.OfficesId);
        await context.SaveChangesAsync();
        return Result.UpdatedResource();
    }

    /// <summary>
    /// Assigns offices to a public holiday.
    /// </summary>
    /// <param name="publicHolidayId">The ID of the public holiday to assign.</param>
    /// <param name="currentOffices">A collection with the offices assigned to a public holiday.</param>
    /// <param name="officesId">A collection of office identifiers obtained from a client.</param>
    private void AssignOfficesToHoliday(
        int publicHolidayId, 
        List<OfficeHoliday> currentOffices, 
        List<int> officesId)
    {
        officeHolidayService.Update(publicHolidayId, ref currentOffices, ref officesId);
    }
}
