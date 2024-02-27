namespace DentallApp.Core.FavoriteDentists.UseCases;

public class CreateFavoriteDentistRequest
{
    public int DentistId { get; init; }
}

public class CreateFavoriteDentistValidator : AbstractValidator<CreateFavoriteDentistRequest>
{
    public CreateFavoriteDentistValidator()
    {
        RuleFor(request => request.DentistId).GreaterThan(0);
    }
}

public class CreateFavoriteDentistUseCase(DbContext context, CreateFavoriteDentistValidator validator)
{
    public async Task<Result<CreatedId>> ExecuteAsync(int userId, CreateFavoriteDentistRequest request)
    {
        var result = validator.Validate(request);
        if (result.IsFailed())
            return result.Invalid();

        var favoriteDentist = new FavoriteDentist
        {
            UserId    = userId,
            DentistId = request.DentistId
        };
        context.Add(favoriteDentist);
        await context.SaveChangesAsync();
        return Result.CreatedResource(favoriteDentist.Id);
    }
}
