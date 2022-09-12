namespace DentallApp.Constants;

public class ResponseMessages
{
    public const string CreateResourceMessage = "El recurso se creó con éxito.";
    public const string UpdateResourceMessage = "El recurso se actualizó con éxito.";
    public const string DeleteResourceMessage = "El recurso se eliminó con éxito.";
    public const string GetResourceMessage = "Recurso obtenido con éxito.";
    public const string ResourceNotFoundMessage = "Recurso no encontrado.";
    public const string ResourceFromAnotherUserMessage = "El recurso es de otro usuario.";

    public const string InactiveUserAccountMessage = "Cuenta de usuario inactiva.";
    public const string EmailSuccessfullyVerifiedMessage = "Correo electrónico verificado con éxito.";
    public const string SuccessfulLoginMessage = "Ha iniciado la sesión con éxito.";
    public const string CouldNotSendEmailMessage = "No se ha podido enviar el correo electrónico.";
    public const string EmailOrPasswordIncorrectMessage = "El correo electrónico o la contraseña son incorrectos.";
    public const string OldPasswordIncorrectMessage = "La contraseña antigua es incorrecta.";
    public const string EmailNotConfirmedMessage = "El correo electrónico no está confirmado.";
    public const string CreateBasicUserAccountMessage = "La cuenta de usuario básico se ha creado con éxito. Por favor, confirme su dirección de correo electrónico.";
    public const string CreateEmployeeAccountMessage = "La cuenta del empleado se ha creado con éxito.";
    public const string UsernameAlreadyExistsMessage = "El correo electrónico que ingresó está siendo usado por otro usuario.";
    public const string AccountAlreadyVerifiedMessage = "La cuenta de usuario ya fue verificada.";
    public const string UsernameNotFoundMessage = "Usuario no encontrado.";
    public const string EmployeeNotFoundMessage = "Empleado no encontrado.";
    public const string AccessTokenInvalidMessage = "El token de acceso es inválido.";
    public const string EmailVerificationTokenInvalidMessage = "El token de verificación email es inválido.";
    public const string PasswordResetTokenInvalidMessage = "El token de restablecimiento de contraseña es inválido.";
    public const string RefreshTokenInvalidMessage = "Refresh-token es inválido.";
    public const string RefreshTokenExpiredMessage = "Refresh-token ha expirado.";
    public const string UpdatedAccessTokenMessage = "Access-token se ha actualizado con éxito.";
    public const string RevokeTokenMessage = "Refresh token ha sido revocado.";
    public const string HasNoRefreshTokenMessage = "No tienes un refresh-token para revocar.";
    public const string SendPasswordResetLinkMessage = "El correo electrónico se envió con éxito para restablecer la contraseña del usuario.";
    public const string PasswordSuccessfullyResetMessage = "La contraseña se ha restablecido con éxito.";
    public const string MissingClaimMessage = "Reclamación (claim) faltante en el token: {0}";
    public const string OfficeNotAssignedMessage = "No puedes gestionar los recursos de una sucursal a la que no ha sido asignada.";
    public const string AppoinmentNotAssignedMessage = "Esta cita no te pertenece.";
    public const string AppoinmentIsAlreadyCanceledMessage = "No puede actualizar una cita que ya está cancelada.";
    public const string CannotRemoveSuperadminMessage = "Lo siento, no se puede eliminar a un SuperAdministrador.";
    public const string CannotEditSuperadminMessage = "Lo siento, no se puede editar los datos de un SuperAdministrador.";
    public const string CannotEditYourOwnProfileMessage = "No puede editar su propio perfil.";
    public const string CannotRemoveYourOwnProfileMessage = "No puede eliminar su propio perfil.";
    public const string PermitsNotGrantedMessage = "No tienes permisos para otorgar esos roles.";
    public const string NotAnImageMessage = "El archivo adjuntado no es una imagen.";
    public const string UnrecognizableFileMessage = "El archivo está irreconocible. No lo puede reconocer el sistema.";
    public const string InvalidModelStateMessage = "Se han producido uno o varios errores de validación.";
    public const string NotAvailableMessage = "N/A";
    public const string OfficeClosedMessage = "Cerrado";
    public const string OfficeClosedForSpecificDayMessage = "El consultorio odontológico está cerrado para el día {0}.";
    public const string DentistNotAvailableMessage = "El odontólogo no está disponible para el día {0}. Por favor, elija otro día.";
    public const string NoMorningOrAfternoonHoursMessage = "El odontólogo no tiene horario de mañana ni de tarde.";
    public const string DentalServiceNotAvailableMessage = "El servicio dental no está disponible. Posiblemente se haya pasado una ID inválida.";
    public const string NoSchedulesAvailableMessage = "No hay horarios disponibles.";
    public const string DateAndTimeAppointmentIsNotAvailableMessage = "La fecha y hora de la cita que has seleccionado no está disponible.";
}
