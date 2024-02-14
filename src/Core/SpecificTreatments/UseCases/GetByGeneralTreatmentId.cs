namespace DentallApp.Core.SpecificTreatments.UseCases;

public class GetTreatmentsByGeneralTreatmentIdResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public double Price { get; init; }
}

public class GetTreatmentsByGeneralTreatmentIdUseCase(DbContext context)
{
    public async Task<IEnumerable<GetTreatmentsByGeneralTreatmentIdResponse>> ExecuteAsync(int generalTreatmentId)
    {
        var specificTreatments = await
            (from specificTreatment in context.Set<SpecificTreatment>()
             join generalTreatment in context.Set<GeneralTreatment>()
             on specificTreatment.GeneralTreatmentId equals generalTreatment.Id
             where specificTreatment.GeneralTreatmentId == generalTreatmentId
             select new GetTreatmentsByGeneralTreatmentIdResponse
             {
                 Id    = specificTreatment.Id,
                 Name  = specificTreatment.Name,
                 Price = specificTreatment.Price
             })
             .AsNoTracking()
             .ToListAsync();

        return specificTreatments;
    }
}
