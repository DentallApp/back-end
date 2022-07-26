namespace DentallApp.Constants;

public class ResponseMessages
{
    public const string GetResourceMessage = "Recurso obtenido con éxito.";
    public const string ResourceNotFoundMessage = "Recurso no encontrado.";

    public const string EmailSuccessfullyVerifiedMessage = "Correo electrónico verificado con éxito.";
    public const string SuccessfulLoginMessage = "Ha iniciado la sesión con éxito.";
    public const string CouldNotSendEmailMessage = "No se ha podido enviar el correo electrónico.";
    public const string EmailOrPasswordIncorrectMessage = "El correo electrónico o la contraseña son incorrectos.";
    public const string EmailNotConfirmedMessage = "El correo electrónico no está confirmado.";
    public const string CreateBasicUserAccountMessage = "La cuenta de usuario básico se ha creado con éxito. Por favor, confirme su dirección de correo electrónico.";
    public const string UsernameAlreadyExistsMessage = "El correo electrónico que ingresó está siendo usado por otro usuario.";
    public const string AccountAlreadyVerifiedMessage = "La cuenta de usuario ya fue verificada.";
    public const string UsernameNotFoundMessage = "Usuario no encontrado.";
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
}
