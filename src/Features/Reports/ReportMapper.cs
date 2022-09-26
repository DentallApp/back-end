namespace DentallApp.Features.Reports;

public static class ReportMapper
{
    public static object MapToObject(this ReportPostTotalAppoinmentDownloadDto reportPostDownloadDto)
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
            reportPostDownloadDto.Appoinments
        };
        return model;
    }

    public static object MapToObject(this ReportPostDentalServiceDto reportPostDto)
    {
        var model = new
        {
            reportPostDto.From,
            reportPostDto.To,
            reportPostDto.OfficeName,
            reportPostDto.Services
        };
        return model;
    }
}
