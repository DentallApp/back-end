﻿namespace DentallApp.Core.Appointments.UseCases;

public class GetAppointmentInformationResponse
{
    public string PatientName { get; init; }
    public string CellPhone { get; init; }
    public string DentistName { get; init; }
    public string OfficeName { get; init; }
    public string DentalServiceName { get; init; }
}

public class SendAppointmentInformationUseCase(
    DbContext context,
    AppSettings settings,
    IInstantMessaging instantMessaging,
    ITreatmentRepository treatmentRepository)
{
    public async Task ExecuteAsync(int appointmentId, CreateAppointmentRequest request)
    {
        // The query is executed in case the scheduling of appointments is done manually by the secretary.
        request.RangeToPay ??= await treatmentRepository.GetRangeToPayAsync(request.GeneralTreatmentId);
        var appointmentInfo = await GetAppointmentInformationAsync(appointmentId);
        var msg = string.Format(Messages.AppointmentInformation,
            appointmentInfo.PatientName,
            settings.BusinessName,
            appointmentInfo.DentistName,
            appointmentInfo.OfficeName,
            appointmentInfo.DentalServiceName,
            request.AppointmentDate.GetDateInSpanishFormat(),
            request.StartHour.GetHourWithoutSeconds(),
            request.RangeToPay?.ToString());
        await instantMessaging.SendMessageAsync(appointmentInfo.CellPhone, msg);
    }

    private Task<GetAppointmentInformationResponse> GetAppointmentInformationAsync(int appointmentId)
    {
        return context.Set<Appointment>()
            .Where(appointment => appointment.Id == appointmentId)
            .Select(appointment => new GetAppointmentInformationResponse
            {
                PatientName       = appointment.Person.FullName,
                CellPhone         = appointment.Person.CellPhone,
                DentistName       = appointment.Employee.Person.FullName,
                DentalServiceName = appointment.GeneralTreatment.Name,
                OfficeName        = appointment.Office.Name
            })
            .IgnoreQueryFilters()
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}
