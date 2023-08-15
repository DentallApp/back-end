namespace DentallApp.Features.GeneralTreatments;

public interface IGeneralTreatmentRepository
{
    Task<GeneralTreatmentGetDurationDto> GetTreatmentWithDurationAsync(int treatmentId);
}
