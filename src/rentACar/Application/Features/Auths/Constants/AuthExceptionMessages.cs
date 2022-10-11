namespace Application.Features.Auths.Constants
{
    public static class AuthExceptionMessages
    {
        public static string EmailAutenticatorNotExistsMessage => "Email authenticator don't exists.";
        public static string OtpAutenticatorNotExistsMessage => "Otp authenticator don't exists.";
        public static string AlreadyVerifiedOtpAuthenticatorExistsMessage => "Already verified otp authenticator is exists.";
        public static string EmailActivationKeyNotExistsMessage => "Email Activation Key don't exists.";
        public static string UserNotExistsMessage => "User don't exists.";
        public static string UserHaveAlreadyAutenticatorMessage => "User have already a authenticator.";
        public static string RefrehTokenNotExistsMessage => "Refresh Token don't exists.";
        public static string InvalidRefreshTokenMessage => "Invalid refresh token.";
        public static string UserMailAlreadyExistsMessage => "User mail already exists.";
        public static string PasswordNotMatchMessage => "Password don't match.";
    }
}
