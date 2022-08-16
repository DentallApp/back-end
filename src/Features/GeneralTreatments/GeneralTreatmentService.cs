namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentService : IGeneralTreatmentService
{
    private readonly IGeneralTreatmentRepository _repository;
    private readonly string _basePath;

    public GeneralTreatmentService(IGeneralTreatmentRepository repository, AppSettings settings)
    {
        _repository = repository;
        _basePath = settings.DentalServicesImagesPath;
    }

    public async Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsWithoutImageUrlAsync()
        => await _repository.GetTreatmentsWithoutImageUrlAsync();

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

    public async Task<Response> CreateTreatmentAsync(GeneralTreatmentInsertDto treatmentInsertDto)
    {
        var treatment = treatmentInsertDto.MapToGeneralTreatment();
        _repository.Insert(treatment);
        await treatmentInsertDto.Image.WriteAsync(Path.Combine(_basePath, treatment.ImageUrl));
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateTreatmentAsync(int id, GeneralTreatmentUpdateDto treatmentUpdateDto)
    {
        var treatment = await _repository.GetByIdAsync(id);
        if (treatment is null)
            return new Response(ResourceNotFoundMessage);

        var oldImageUrl = treatment.ImageUrl;
        treatmentUpdateDto.MapToGeneralTreatment(treatment);
        if (treatmentUpdateDto.Image is not null)
        {
            File.Delete(Path.Combine(_basePath, oldImageUrl));
            await treatmentUpdateDto.Image.WriteAsync(Path.Combine(_basePath, treatment.ImageUrl));
        }
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> RemoveTreatmentAsync(int id)
    {
        var treatment = await _repository.GetByIdAsync(id);
        if (treatment is null)
            return new Response(ResourceNotFoundMessage);

        _repository.Delete(treatment);
        await _repository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
