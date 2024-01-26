﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DentallApp.Shared.Resources.ApiResponses {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DentallApp.Shared.Resources.ApiResponses.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El token de acceso es inválido..
        /// </summary>
        public static string AccessTokenInvalid {
            get {
                return ResourceManager.GetString("AccessTokenInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La cuenta de usuario ya fue verificada..
        /// </summary>
        public static string AccountAlreadyVerified {
            get {
                return ResourceManager.GetString("AccountAlreadyVerified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estimado usuario {0}, su cita agendada en el consultorio odontológico {1} para el día {2} a las {3} ha sido cancelada por el siguiente motivo: {4}.
        /// </summary>
        public static string AppointmentCancellation {
            get {
                return ResourceManager.GetString("AppointmentCancellation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede cambiar el estado de la cita del día anterior a la fecha actual..
        /// </summary>
        public static string AppointmentCannotBeUpdatedForPreviousDays {
            get {
                return ResourceManager.GetString("AppointmentCannotBeUpdatedForPreviousDays", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La fecha de la cita que seleccionaste no está disponible porque es día festivo..
        /// </summary>
        public static string AppointmentDateIsPublicHoliday {
            get {
                return ResourceManager.GetString("AppointmentDateIsPublicHoliday", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hola {0}, gracias por agendar una cita en el consultorio {1}. La información de su cita es:\n- Odontólogo: {2}\n- Consultorio: {3}\n- Servicio dental: {4}\n- Fecha de la cita: {5}\n- Hora de la cita: {6}\n{7}.
        /// </summary>
        public static string AppointmentInformation {
            get {
                return ResourceManager.GetString("AppointmentInformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede actualizar una cita que ya está cancelada..
        /// </summary>
        public static string AppointmentIsAlreadyCanceled {
            get {
                return ResourceManager.GetString("AppointmentIsAlreadyCanceled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Esta cita no te pertenece..
        /// </summary>
        public static string AppointmentNotAssigned {
            get {
                return ResourceManager.GetString("AppointmentNotAssigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Estimado usuario {0}, le recordamos que el día {1} a las {2} tiene una cita agendada en el consultorio {3} con el odontólogo {4}.
        /// </summary>
        public static string AppointmentReminder {
            get {
                return ResourceManager.GetString("AppointmentReminder", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede cancelar una cita que ya pasó. Nuestros empleados se encargarán de cambiar el estado de esta cita posteriormente..
        /// </summary>
        public static string AppointmentThatHasAlreadyPassedBasicUser {
            get {
                return ResourceManager.GetString("AppointmentThatHasAlreadyPassedBasicUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Algunas citas no se pudieron cancelar porque ya pasó la fecha y hora estipulada. Conteo: {0}..
        /// </summary>
        public static string AppointmentThatHasAlreadyPassedEmployee {
            get {
                return ResourceManager.GetString("AppointmentThatHasAlreadyPassedEmployee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lo siento, no se puede editar los datos de un SuperAdministrador..
        /// </summary>
        public static string CannotEditSuperadmin {
            get {
                return ResourceManager.GetString("CannotEditSuperadmin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede editar su propio perfil..
        /// </summary>
        public static string CannotEditYourOwnProfile {
            get {
                return ResourceManager.GetString("CannotEditYourOwnProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lo siento, no se puede eliminar a un SuperAdministrador..
        /// </summary>
        public static string CannotRemoveSuperadmin {
            get {
                return ResourceManager.GetString("CannotRemoveSuperadmin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede eliminar su propio perfil..
        /// </summary>
        public static string CannotRemoveYourOwnProfile {
            get {
                return ResourceManager.GetString("CannotRemoveYourOwnProfile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puede actualizar otro recurso de usuario..
        /// </summary>
        public static string CannotUpdateAnotherUserResource {
            get {
                return ResourceManager.GetString("CannotUpdateAnotherUserResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sólo puede acceder a sus propias citas..
        /// </summary>
        public static string CanOnlyAccessYourOwnAppointments {
            get {
                return ResourceManager.GetString("CanOnlyAccessYourOwnAppointments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se ha podido enviar el correo electrónico..
        /// </summary>
        public static string CouldNotSendEmail {
            get {
                return ResourceManager.GetString("CouldNotSendEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La cuenta de usuario básico se ha creado con éxito. Por favor, confirme su dirección de correo electrónico..
        /// </summary>
        public static string CreateBasicUserAccount {
            get {
                return ResourceManager.GetString("CreateBasicUserAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La cuenta del empleado se ha creado con éxito..
        /// </summary>
        public static string CreateEmployeeAccount {
            get {
                return ResourceManager.GetString("CreateEmployeeAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El recurso se creó con éxito..
        /// </summary>
        public static string CreateResource {
            get {
                return ResourceManager.GetString("CreateResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La fecha y hora de la cita que has seleccionado no está disponible..
        /// </summary>
        public static string DateAndTimeAppointmentIsNotAvailable {
            get {
                return ResourceManager.GetString("DateAndTimeAppointmentIsNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El recurso se eliminó con éxito..
        /// </summary>
        public static string DeleteResource {
            get {
                return ResourceManager.GetString("DeleteResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El servicio dental no está disponible. Posiblemente se haya pasado una ID inválida..
        /// </summary>
        public static string DentalServiceNotAvailable {
            get {
                return ResourceManager.GetString("DentalServiceNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El odontólogo no está disponible para el día {0}. Por favor, elija otro día..
        /// </summary>
        public static string DentistNotAvailable {
            get {
                return ResourceManager.GetString("DentistNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error en la llamada a la API del token de Direct Line..
        /// </summary>
        public static string DirectLineTokenFailed {
            get {
                return ResourceManager.GetString("DirectLineTokenFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El correo electrónico no está confirmado..
        /// </summary>
        public static string EmailNotConfirmed {
            get {
                return ResourceManager.GetString("EmailNotConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El correo electrónico o la contraseña son incorrectos..
        /// </summary>
        public static string EmailOrPasswordIncorrect {
            get {
                return ResourceManager.GetString("EmailOrPasswordIncorrect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Correo electrónico verificado con éxito..
        /// </summary>
        public static string EmailSuccessfullyVerified {
            get {
                return ResourceManager.GetString("EmailSuccessfullyVerified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El token de verificación email es inválido..
        /// </summary>
        public static string EmailVerificationTokenInvalid {
            get {
                return ResourceManager.GetString("EmailVerificationTokenInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Empleado no encontrado..
        /// </summary>
        public static string EmployeeNotFound {
            get {
                return ResourceManager.GetString("EmployeeNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recurso obtenido con éxito..
        /// </summary>
        public static string GetResource {
            get {
                return ResourceManager.GetString("GetResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No tienes un refresh-token para revocar..
        /// </summary>
        public static string HasNoRefreshToken {
            get {
                return ResourceManager.GetString("HasNoRefreshToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cuenta de usuario inactiva..
        /// </summary>
        public static string InactiveUserAccount {
            get {
                return ResourceManager.GetString("InactiveUserAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se han producido uno o varios errores de validación..
        /// </summary>
        public static string InvalidModelState {
            get {
                return ResourceManager.GetString("InvalidModelState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo adjuntado no es una imagen..
        /// </summary>
        public static string IsNotImage {
            get {
                return ResourceManager.GetString("IsNotImage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reclamación (claim) faltante en el token: {0}.
        /// </summary>
        public static string MissingClaim {
            get {
                return ResourceManager.GetString("MissingClaim", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El odontólogo no tiene horario de mañana ni de tarde..
        /// </summary>
        public static string NoMorningOrAfternoonHours {
            get {
                return ResourceManager.GetString("NoMorningOrAfternoonHours", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No hay horarios disponibles..
        /// </summary>
        public static string NoSchedulesAvailable {
            get {
                return ResourceManager.GetString("NoSchedulesAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cerrado.
        /// </summary>
        public static string OfficeClosed {
            get {
                return ResourceManager.GetString("OfficeClosed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El consultorio odontológico está cerrado para el día {0}..
        /// </summary>
        public static string OfficeClosedForSpecificDay {
            get {
                return ResourceManager.GetString("OfficeClosedForSpecificDay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No puedes gestionar los recursos de una sucursal a la que no ha sido asignada..
        /// </summary>
        public static string OfficeNotAssigned {
            get {
                return ResourceManager.GetString("OfficeNotAssigned", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La contraseña antigua es incorrecta..
        /// </summary>
        public static string OldPasswordIncorrect {
            get {
                return ResourceManager.GetString("OldPasswordIncorrect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El token de restablecimiento de contraseña es inválido..
        /// </summary>
        public static string PasswordResetTokenInvalid {
            get {
                return ResourceManager.GetString("PasswordResetTokenInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La contraseña se ha restablecido con éxito..
        /// </summary>
        public static string PasswordSuccessfullyReset {
            get {
                return ResourceManager.GetString("PasswordSuccessfullyReset", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No tienes permisos para otorgar esos roles..
        /// </summary>
        public static string PermitsNotGranted {
            get {
                return ResourceManager.GetString("PermitsNotGranted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El valor a pagar es de ${0}.
        /// </summary>
        public static string RangeToPay {
            get {
                return ResourceManager.GetString("RangeToPay", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El rango a pagar es de ${0} a ${1}.
        /// </summary>
        public static string RangeToPayMinMax {
            get {
                return ResourceManager.GetString("RangeToPayMinMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refresh-token ha expirado..
        /// </summary>
        public static string RefreshTokenExpired {
            get {
                return ResourceManager.GetString("RefreshTokenExpired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refresh-token es inválido..
        /// </summary>
        public static string RefreshTokenInvalid {
            get {
                return ResourceManager.GetString("RefreshTokenInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El recurso es de otro usuario..
        /// </summary>
        public static string ResourceFromAnotherUser {
            get {
                return ResourceManager.GetString("ResourceFromAnotherUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Recurso no encontrado..
        /// </summary>
        public static string ResourceNotFound {
            get {
                return ResourceManager.GetString("ResourceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Refresh token ha sido revocado..
        /// </summary>
        public static string RevokeToken {
            get {
                return ResourceManager.GetString("RevokeToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja una fecha válida..
        /// </summary>
        public static string SelectAppointmentDate {
            get {
                return ResourceManager.GetString("SelectAppointmentDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja un servicio dental..
        /// </summary>
        public static string SelectDentalService {
            get {
                return ResourceManager.GetString("SelectDentalService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja un odontólogo..
        /// </summary>
        public static string SelectDentist {
            get {
                return ResourceManager.GetString("SelectDentist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja un consultorio..
        /// </summary>
        public static string SelectOffice {
            get {
                return ResourceManager.GetString("SelectOffice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja un paciente..
        /// </summary>
        public static string SelectPatient {
            get {
                return ResourceManager.GetString("SelectPatient", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error. Escoja un horario..
        /// </summary>
        public static string SelectSchedule {
            get {
                return ResourceManager.GetString("SelectSchedule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El correo electrónico se envió con éxito para restablecer la contraseña del usuario..
        /// </summary>
        public static string SendPasswordResetLink {
            get {
                return ResourceManager.GetString("SendPasswordResetLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El odontólogo atiende los {0}.
        /// </summary>
        public static string ShowScheduleToUser {
            get {
                return ResourceManager.GetString("ShowScheduleToUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ha iniciado la sesión con éxito..
        /// </summary>
        public static string SuccessfulLogin {
            get {
                return ResourceManager.GetString("SuccessfulLogin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Las citas se cancelaron con éxito..
        /// </summary>
        public static string SuccessfullyCancelledAppointments {
            get {
                return ResourceManager.GetString("SuccessfullyCancelledAppointments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cita agendada con éxito. {0}..
        /// </summary>
        public static string SuccessfullyScheduledAppointment {
            get {
                return ResourceManager.GetString("SuccessfullyScheduledAppointment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Gracias por usar nuestro servicio.\n\nSi desea agendar otra cita, escriba algo para empezar de nuevo el proceso..
        /// </summary>
        public static string ThanksForUsingService {
            get {
                return ResourceManager.GetString("ThanksForUsingService", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total de horas disponibles: {0}.\n\nSeleccione la hora para su cita:.
        /// </summary>
        public static string TotalHoursAvailable {
            get {
                return ResourceManager.GetString("TotalHoursAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hubo un error inesperado, por favor intente de nuevo..
        /// </summary>
        public static string UnexpectedError {
            get {
                return ResourceManager.GetString("UnexpectedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se encontraron errores inesperados, por favor intente de nuevo..
        /// </summary>
        public static string UnexpectedErrors {
            get {
                return ResourceManager.GetString("UnexpectedErrors", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Se detectó una violación a una restricción única (UNIQUE INDEX). Por favor, no envíe una entrada duplicada..
        /// </summary>
        public static string UniqueConstraintViolated {
            get {
                return ResourceManager.GetString("UniqueConstraintViolated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El archivo está irreconocible. No lo puede reconocer el sistema..
        /// </summary>
        public static string UnrecognizableFile {
            get {
                return ResourceManager.GetString("UnrecognizableFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Access-token se ha actualizado con éxito..
        /// </summary>
        public static string UpdatedAccessToken {
            get {
                return ResourceManager.GetString("UpdatedAccessToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El recurso se actualizó con éxito..
        /// </summary>
        public static string UpdateResource {
            get {
                return ResourceManager.GetString("UpdateResource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El correo electrónico que ingresó está siendo usado por otro usuario..
        /// </summary>
        public static string UsernameAlreadyExists {
            get {
                return ResourceManager.GetString("UsernameAlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario no encontrado..
        /// </summary>
        public static string UsernameNotFound {
            get {
                return ResourceManager.GetString("UsernameNotFound", resourceCulture);
            }
        }
    }
}