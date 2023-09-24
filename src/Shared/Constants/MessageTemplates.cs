namespace DentallApp.Shared.Constants;

public class MessageTemplates
{
    public const string AppointmentCancellationMessageTemplate = 
        "Estimado usuario {0}, su cita agendada en el consultorio odontológico {1} para el día {2} a las {3} ha sido cancelada por el siguiente motivo: {4}";

    public const string AppointmentReminderMessageTemplate =
        "Estimado usuario {0}, le recordamos que el día {1} a las {2} tiene una cita agendada en el consultorio {3} con el odontólogo {4}";

    public const string AppointmentInformationMessageTemplate =
        "Hola {0}, gracias por agendar una cita en el consultorio {1}. La información de su cita es:" +
        "\n- Odontólogo: {2}" +
        "\n- Consultorio: {3}" +
        "\n- Servicio dental: {4}" +
        "\n- Fecha de la cita: {5}" +
        "\n- Hora de la cita: {6}" +
        "\n{7}";

}
