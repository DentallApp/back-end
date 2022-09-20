namespace DentallApp.Features.Reports;

public static class ReportMapper
{
    [Decompile]
    public static ReportGetAppoinmentDto MapToReportGetAppoinmentDto(this Appoinment appoinment)
        => new()
        {
            AppoinmentDate    = appoinment.Date.GetDateWithStandardFormat(),
            StartHour         = appoinment.StartHour.GetHourWithoutSeconds(),
            PatientName       = appoinment.Person.FullName,
            DentalServiceName = appoinment.GeneralTreatment.Name,
            DentistName       = appoinment.Employee.Person.FullName,
            OfficeName        =  appoinment.Office.Name,
            AppoinmentStatus  = appoinment.AppoinmentStatus.Name
        };

    public static object MapToObject(this ReportPostAppoinmentDownloadDto reportPostDownloadDto)
    {
        var model = new
        {
            reportPostDownloadDto.From,
            reportPostDownloadDto.To,
            reportPostDownloadDto.Appoinments
        };
        return model;
    }
}
