using System.Security.Cryptography;

namespace AuthServiceYourVoice.Application.Services;

public static class TokenGeneratorService
{
    public static string GenerateEmailVerificationToken()
    {
        return GenerateSecureToken(32);
    }

    public static string GeneratePasswordResetToken()
    {
        return GenerateSecureToken(32);
    }

    private static string GenerateSecureToken(int length)
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return Convert.ToBase64String(bytes).Replace("+", "-").Replace("/", "_").Replace("=", "");
    }

    public static string GenerateTwoFactorCode()
    {
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[4];
        rng.GetBytes(bytes);
        return BitConverter.ToString(bytes).Replace("-", "").Substring(0, 8);
    }
}