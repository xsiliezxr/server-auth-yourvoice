using AuthServiceYourVoice.Application.DTOs;
using AuthServiceYourVoice.Application.DTOs.Email;

namespace AuthServiceYourVoice.Application.Interfaces;

public interface IAuthService
{
    Task<RegisterResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<EmailResponseDto> VerifyEmailAsync(VerifyEmailDto verifyEmailDto);
    Task<EmailResponseDto> ResendVerificationEmailAsync(ResendVerificationDto resendDto);
    Task<EmailResponseDto> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<EmailResponseDto> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
    Task<UserResponseDto?> GetUserByIdAsync(string userId);
    Task<AuthResponseDto> VerifyTwoFactorAsync(string id, VerifyTwoFactorDto verifyTwoFactorDto);
    Task<AuthResponseDto> ChangeTwoFactorStatusByIdAsync(string userId, ChangeTwoFactorDto changeTwoFactorDto);

}