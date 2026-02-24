using Org.BouncyCastle.Tls;

namespace AuthServiceYourVoice.Application.Exceptions;

public static class ErrorCodes
{
    public const string EMAIL_ALREADY_EXISTS = "EMAIL_ALREADY_EXISTS";
    public const string USERNAME_ALREADY_EXISTS = "USERNAME_ALREADY_EXISTS";
    public const string INVALID_CREDENTIALS = "INVALID_CREDENTIALS";
    public const string USER_ACCOUNT_DISABLED = "USER_ACCOUNT_DISABLED";
    public const string IMAGE_UPLOAD_FAILED = "IMAGE_UPLOAD_FAILED";
    public const string INVALID_FILE_FORMAT = "INVALID_FILE_FORMAT";
    public const string FILE_TOO_LARGE = "FILE_TOO_LARGE";
    public const string TWO_FACTOR_CODE_INVALID = "TWO_FACTOR_CODE_INVALID";
    public const string TWO_FACTOR_CODE_ERROR = "TWO_FACTOR_CODE_ERROR";
}