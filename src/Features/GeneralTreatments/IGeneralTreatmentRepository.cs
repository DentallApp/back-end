namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentRepository : IRepository<GeneralTreatment>
{
    Task<IEnumerable<GeneralTreatmentGetDto>> GetTreatmentsAsync();
    Task<IEnumerable<GeneralTreatmentShowDto>> GetTreatmentsWithoutImageUrlAsync();
    Task<IEnumerable<GeneralTreatmentGetNameDto>> GetTreatmentsWithNameAsync();
    Task<GeneralTreatmentGetDurationDto> GetTreatmentWithDurationAsync(int treatmentId);
}
