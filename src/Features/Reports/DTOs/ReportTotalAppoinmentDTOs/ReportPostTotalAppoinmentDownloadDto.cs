﻿namespace DentallApp.Features.Reports.DTOs;

public class ReportPostTotalAppoinmentDownloadDto
{
    public string From { get; set; }
    public string To { get; set; }
    public string OfficeName { get; set; }
    public string DentistName { get; set; }
    public ReportGetTotalAppoinmentDto Totals { get; set; }
}
