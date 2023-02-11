namespace DentallApp.Features.Reports;

public static class ReportMapper
{
    public static object MapToObject(this ReportPostTotalAppointmentDownloadDto reportPostDownloadDto)
    {
        var model = new
        {
            reportPostDownloadDto.From,
            reportPostDownloadDto.To,
            reportPostDownloadDto.OfficeName,
            reportPostDownloadDto.DentistName,
            reportPostDownloadDto.Totals
        };
        return model;
    }

    public static object MapToObject(this ReportPostScheduledDownloadDto reportPostDownloadDto)
    {
        var model = new
        {
            reportPostDownloadDto.From,
            reportPostDownloadDto.To,
            reportPostDownloadDto.Appointments
        };
        return model;
    }

    public static object MapToObject(this ReportDentalServicesDownloadRequest request)
    {
        var model = new
        {
            request.From,
            request.To,
            request.OfficeName,
            request.Services
        };
        return model;
    }
}
