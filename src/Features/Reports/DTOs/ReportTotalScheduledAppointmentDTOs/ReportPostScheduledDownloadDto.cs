﻿namespace DentallApp.Features.Reports.DTOs;

public class ReportPostScheduledDownloadDto
{
    public string From { get; set; }
    public string To { get; set; }
    public IEnumerable<ReportGetTotalScheduledAppointmentDto> Appointments { get; set; }
}