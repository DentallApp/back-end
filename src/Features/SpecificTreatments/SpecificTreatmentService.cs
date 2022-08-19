namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatmentService : ISpecificTreatmentService
{
    private readonly ISpecificTreatmentRepository _repository;

    public SpecificTreatmentService(ISpecificTreatmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> CreateSpecificTreatmentAsync(SpecificTreatmentInsertDto treatmentInsertDto)
    {
        var specificTreatment = treatmentInsertDto.MapToSpecificTreatment();
        _repository.Insert(specificTreatment);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateSpecificTreatmentAsync(int id, SpecificTreatmentUpdateDto treatmentUpdateDto)
    {
        var specificTreatment = await _repository.GetByIdAsync(id);
        if (specificTreatment is null)
            return new Response(ResourceNotFoundMessage);

        treatmentUpdateDto.MapToSpecificTreatment(specificTreatment);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<IEnumerable<SpecificTreatmentShowDto>> GetSpecificTreatmentsAsync()
        => await _repository.GetSpecificTreatmentsAsync();

    public async Task<IEnumerable<SpecificTreatmentGetDto>> GetSpecificTreatmentsByGeneralTreatmentIdAsync(int generalTreatmentId)
        => await _repository.GetSpecificTreatmentsByGeneralTreatmentIdAsync(generalTreatmentId);

    public async Task<Response> RemoveSpecificTreatmentAsync(int id)
    {
        var specificTreatment = await _repository.GetByIdAsync(id);
        if (specificTreatment is null)
            return new Response(ResourceNotFoundMessage);

        _repository.Delete(specificTreatment);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
