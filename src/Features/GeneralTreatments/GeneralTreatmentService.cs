namespace DentallApp.Features.GeneralTreatments;

public class GeneralTreatmentService : IGeneralTreatmentService
{
    private readonly IGeneralTreatmentRepository _treatmentRepository;
    private readonly string _basePath;

    public GeneralTreatmentService(IGeneralTreatmentRepository treatmentRepository, AppSettings settings)
    {
        _treatmentRepository = treatmentRepository;
        _basePath = settings.DentalServicesImagesPath;
    }

    public async Task<Response<GeneralTreatmentGetDto>> GetTreatmentByIdAsync(int id)
    {
        var treatment = await _treatmentRepository.GetByIdAsync(id);
        if (treatment is null)
            return new Response<GeneralTreatmentGetDto>(ResourceNotFoundMessage);

        return new Response<GeneralTreatmentGetDto>()
        {
            Success = true,
            Data = treatment.MapToGeneralTreatmentGetDto(),
            Message = GetResourceMessage
        };
    }

    public async Task<Response<DtoBase>> CreateTreatmentAsync(GeneralTreatmentInsertDto treatmentInsertDto)
    {
        var treatment = treatmentInsertDto.MapToGeneralTreatment();
        _treatmentRepository.Insert(treatment);
        await treatmentInsertDto.Image.WriteAsync(Path.Combine(_basePath, treatment.ImageUrl));
        await _treatmentRepository.SaveAsync();

        return new Response<DtoBase>
        {
            Data    = new DtoBase { Id = treatment.Id },
            Success = true,
            Message = CreateResourceMessage
        };
    }

    public async Task<Response> UpdateTreatmentAsync(int id, GeneralTreatmentUpdateDto treatmentUpdateDto)
    {
        var treatment = await _treatmentRepository.GetByIdAsync(id);
        if (treatment is null)
            return new Response(ResourceNotFoundMessage);

        var oldImageUrl = treatment.ImageUrl;
        treatmentUpdateDto.MapToGeneralTreatment(treatment);
        if (treatmentUpdateDto.Image is not null)
        {
            File.Delete(Path.Combine(_basePath, oldImageUrl));
            await treatmentUpdateDto.Image.WriteAsync(Path.Combine(_basePath, treatment.ImageUrl));
        }
        await _treatmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = UpdateResourceMessage
        };
    }

    public async Task<Response> RemoveTreatmentAsync(int id)
    {
        var treatment = await _treatmentRepository.GetByIdAsync(id);
        if (treatment is null)
            return new Response(ResourceNotFoundMessage);

        _treatmentRepository.SoftDelete(treatment);
        await _treatmentRepository.SaveAsync();

        return new Response
        {
            Success = true,
            Message = DeleteResourceMessage
        };
    }
}
