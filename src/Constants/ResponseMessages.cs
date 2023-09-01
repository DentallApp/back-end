namespace DentallApp.Constants;

public class ResponseMessages
{
    public const string CreateResourceMessage                              = "El recurso se creó con éxito.";
    public const string UpdateResourceMessage                              = "El recurso se actualizó con éxito.";
    public const string DeleteResourceMessage                              = "El recurso se eliminó con éxito.";
    public const string GetResourceMessage                                 = "Recurso obtenido con éxito.";
    public const string ResourceNotFoundMessage                            = "Recurso no encontrado.";
    public const string ResourceFromAnotherUserMessage                     = "El recurso es de otro usuario.";
    public const string CannotUpdateAnotherUserResource                    = "No puede actualizar otro recurso de usuario.";

    public const string UnexpectedErrorMessage                             = "Hubo un error inesperado, por favor intente de nuevo.";
    public const string UnexpectedErrorsMessage                            = "Se encontraron errores inesperados, por favor intente de nuevo.";
    public const string UniqueConstraintViolatedMessage                    = "Se detectó una violación a una restricción única (UNIQUE INDEX). " +
                                                                             "Por favor, no envíe una entrada duplicada.";

    public const string DirectLineTokenFailedMessage                       = "Direct Line token API call failed.";
    public const string InactiveUserAccountMessage                         = "Cuenta de usuario inactiva.";
    public const string EmailSuccessfullyVerifiedMessage                   = "Correo electrónico verificado con éxito.";
    public const string SuccessfulLoginMessage                             = "Ha iniciado la sesión con éxito.";
    public const string CouldNotSendEmailMessage                           = "No se ha podido enviar el correo electrónico.";
    public const string EmailOrPasswordIncorrectMessage                    = "El correo electrónico o la contraseña son incorrectos.";
    public const string OldPasswordIncorrectMessage                        = "La contraseña antigua es incorrecta.";
    public const string EmailNotConfirmedMessage                           = "El correo electrónico no está confirmado.";
    public const string CreateBasicUserAccountMessage                      = "La cuenta de usuario básico se ha creado con éxito. Por favor, confirme su dirección de correo electrónico.";
    public const string CreateEmployeeAccountMessage                       = "La cuenta del empleado se ha creado con éxito.";
    public const string UsernameAlreadyExistsMessage                       = "El correo electrónico que ingresó está siendo usado por otro usuario.";
    public const string AccountAlreadyVerifiedMessage                      = "La cuenta de usuario ya fue verificada.";
    public const string UsernameNotFoundMessage                            = "Usuario no encontrado.";
    public const string EmployeeNotFoundMessage                            = "Empleado no encontrado.";
    public const string AccessTokenInvalidMessage                          = "El token de acceso es inválido.";
    public const string EmailVerificationTokenInvalidMessage               = "El token de verificación email es inválido.";
    public const string PasswordResetTokenInvalidMessage                   = "El token de restablecimiento de contraseña es inválido.";
    public const string RefreshTokenInvalidMessage                         = "Refresh-token es inválido.";
    public const string RefreshTokenExpiredMessage                         = "Refresh-token ha expirado.";
    public const string UpdatedAccessTokenMessage                          = "Access-token se ha actualizado con éxito.";
    public const string RevokeTokenMessage                                 = "Refresh token ha sido revocado.";
    public const string HasNoRefreshTokenMessage                           = "No tienes un refresh-token para revocar.";
    public const string SendPasswordResetLinkMessage                       = "El correo electrónico se envió con éxito para restablecer la contraseña del usuario.";
    public const string PasswordSuccessfullyResetMessage                   = "La contraseña se ha restablecido con éxito.";
    public const string MissingClaimMessage                                = "Reclamación (claim) faltante en el token: {0}";
    public const string OfficeNotAssignedMessage                           = "No puedes gestionar los recursos de una sucursal a la que no ha sido asignada.";
    public const string AppointmentNotAssignedMessage                      = "Esta cita no te pertenece.";
    public const string SuccessfullyCancelledAppointmentsMessage           = "Las citas se cancelaron con éxito.";
    public const string AppointmentIsAlreadyCanceledMessage                = "No puede actualizar una cita que ya está cancelada.";
    public const string CanOnlyAccessYourOwnAppointmentsMessage            = "Sólo puede acceder a sus propias citas.";
    public const string AppointmentThatHasAlreadyPassedBasicUserMessage    = "No puede cancelar una cita que ya pasó. Nuestros empleados se encargarán de cambiar el estado de esta cita posteriormente.";
    public const string AppointmentThatHasAlreadyPassedEmployeeMessage     = "Algunas citas no se pudieron cancelar porque ya pasó la fecha y hora estipulada. Conteo: {0}.";
    public const string AppointmentCannotBeUpdatedForPreviousDaysMessage   = "No puede cambiar el estado de la cita del día anterior a la fecha actual.";
    public const string CannotRemoveSuperadminMessage                      = "Lo siento, no se puede eliminar a un SuperAdministrador.";
    public const string CannotEditSuperadminMessage                        = "Lo siento, no se puede editar los datos de un SuperAdministrador.";
    public const string CannotEditYourOwnProfileMessage                    = "No puede editar su propio perfil.";
    public const string CannotRemoveYourOwnProfileMessage                  = "No puede eliminar su propio perfil.";
    public const string PermitsNotGrantedMessage                           = "No tienes permisos para otorgar esos roles.";
    public const string NotAnImageMessage                                  = "El archivo adjuntado no es una imagen.";
    public const string UnrecognizableFileMessage                          = "El archivo está irreconocible. No lo puede reconocer el sistema.";
    public const string InvalidModelStateMessage                           = "Se han producido uno o varios errores de validación.";
    public const string NotAvailableMessage                                = "N/A";
    public const string OfficeClosedMessage                                = "Cerrado";
    public const string OfficeClosedForSpecificDayMessage                  = "El consultorio odontológico está cerrado para el día {0}.";
    public const string DentistNotAvailableMessage                         = "El odontólogo no está disponible para el día {0}. Por favor, elija otro día.";
    public const string NoMorningOrAfternoonHoursMessage                   = "El odontólogo no tiene horario de mañana ni de tarde.";
    public const string DentalServiceNotAvailableMessage                   = "El servicio dental no está disponible. Posiblemente se haya pasado una ID inválida.";
    public const string NoSchedulesAvailableMessage                        = "No hay horarios disponibles.";
    public const string DateAndTimeAppointmentIsNotAvailableMessage        = "La fecha y hora de la cita que has seleccionado no está disponible.";
    public const string AppointmentDateIsPublicHolidayMessage              = "La fecha de la cita que seleccionaste no está disponible porque es día festivo.";

    // Messages sent by the bot.
    public const string SelectDentalServiceMessage                         = "Error. Escoja un servicio dental";
    public const string SelectDentistMessage                               = "Error. Escoja un odontólogo";
    public const string SelectPatientMessage                               = "Error. Escoja un paciente";
    public const string SelectOfficeMessage                                = "Error. Escoja un consultorio";
    public const string SelectAppointmentDateMessage                       = "Error. Escoja una fecha válida";
    public const string SelectScheduleMessage                              = "Error. Escoja un horario";
    public const string ShowScheduleToUserMessage                          = "El odontólogo atiende los {0}";
    public const string RangeToPayMinMaxMessage                            = "El rango a pagar es de ${0} a ${1}";
    public const string RangeToPayMessage                                  = "El valor a pagar es de ${0}";
    public const string SuccessfullyScheduledAppointmentMessage            = "Cita agendada con éxito. {0}.";
    public const string TotalHoursAvailableMessage                         = "Total de horas disponibles: {0}.\n\nSeleccione la hora para su cita:";
    public const string ThanksForUsingServiceMessage                       = "Gracias por usar nuestro servicio.\n\n" +
                                                                                "Si desea agendar otra cita, escriba algo para empezar de nuevo el proceso.";
}
