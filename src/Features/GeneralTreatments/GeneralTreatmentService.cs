namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentService : IGeneralTreatmentService
{
    private readonly IGeneralTreatmentRepository _repository;

    public GeneralTreatmentService(IGeneralTreatmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatments()
        => await _repository.GetTreatments();

    public async Task<Response<GeneralTreatmentGetDto>> GetTreatmentById(int id)
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
