namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentService : IGeneralTreatmentService
{
    private readonly IGeneralTreatmentRepository _repository;

    public GeneralTreatmentService(IGeneralTreatmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync()
        => await _repository.GetTreatmentsAsync();

    public async Task<Response<GeneralTreatmentGetDto>> GetTreatmentByIdAsync(int id)
    {
        var treatment = await _repository.GetByIdAsync(id);
        if (treatment is null)
            return new Response<GeneralTreatmentGetDto>(ResourceNotFoundMessage);

        return new Response<GeneralTreatmentGetDto>()
        {
            Success = true,
            Data = treatment.MapToGeneralTreatmentGetDto(),
            Message = GetResourceMessage
        };
    }
}
