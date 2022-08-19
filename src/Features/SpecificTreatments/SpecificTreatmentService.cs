namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatmentService : ISpecificTreatmentService
{
    private readonly ISpecificTreatmentRepository _repository;

    public SpecificTreatmentService(ISpecificTreatmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SpecificTreatmentShowDto>> GetSpecificTreatmentsAsync()
        => await _repository.GetSpecificTreatmentsAsync();

    public async Task<IEnumerable<SpecificTreatmentGetDto>> GetSpecificTreatmentsByGeneralTreatmentIdAsync(int generalTreatmentId)
        => await _repository.GetSpecificTreatmentsByGeneralTreatmentIdAsync(generalTreatmentId);
}
