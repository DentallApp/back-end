namespace DentallApp.Features.SpecificTreatments;

public class SpecificTreatmentService : ISpecificTreatmentService
{
    private readonly ISpecificTreatmentRepository _treatmentRepository;

    public SpecificTreatmentService(ISpecificTreatmentRepository treatmentRepository)
    {
        _treatmentRepository = treatmentRepository;
    }

    public async Task<Response<DtoBase>> CreateSpecificTreatmentAsync(SpecificTreatmentInsertDto treatmentInsertDto)
    {
        var specificTreatment = treatmentInsertDto.MapToSpecificTreatment();
        _treatmentRepository.Insert(specificTreatment);
        await _treatmentRepository.SaveAsync();

        return new Response<DtoBase>
        {
            Data    = new DtoBase { Id = specificTreatment.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateSpecificTreatmentAsync(int id, SpecificTreatmentUpdateDto treatmentUpdateDto)
    {
        var specificTreatment = await _treatmentRepository.GetByIdAsync(id);
        if (specificTreatment is null)
            return new Response(ResourceNotFoundMessage);

        treatmentUpdateDto.MapToSpecificTreatment(specificTreatment);
        await _treatmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> RemoveSpecificTreatmentAsync(int id)
    {
        var specificTreatment = await _treatmentRepository.GetByIdAsync(id);
        if (specificTreatment is null)
            return new Response(ResourceNotFoundMessage);

        _treatmentRepository.Delete(specificTreatment);
        await _treatmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
