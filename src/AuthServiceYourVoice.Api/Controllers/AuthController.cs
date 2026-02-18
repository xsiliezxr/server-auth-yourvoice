using System;
using AuthServiceYourVoice.Application.DTOs;
using AuthServiceYourVoice.Application.DTOs.Email;
using AuthServiceYourVoice.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace AuthServiceYourVoice.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<object>> GetProfile()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "sub" || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
        if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
        {
            return Unauthorized();
        }

        var user = await authService.GetUserByIdAsync(userIdClaim.Value);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new
        {
            success = true,
            message = "Perfil obtenido exitosamente",
            data = user
        });
    }
    [HttpPost("register")]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10MB límite
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<RegisterResponseDto>> Register([FromForm] RegisterDto registerDto)
    {
        var result = await authService.RegisterAsync(registerDto);
        // Devolver 201 Created para registro
        return StatusCode(201, result);
    }

    [HttpPost("login")]
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        var result = await authService.LoginAsync(loginDto);

        if (result.RequiresTwoFactor)
        {
            return Accepted(new
            {
                success = true,
                message = "Se requiere autenticación de dos factores",
            });
        }

        return Ok(result);
    }

    [HttpPost("verify-email")]
    [EnableRateLimiting("ApiPolicy")]
    public async Task<ActionResult<EmailResponseDto>> VerifyEmail([FromBody] VerifyEmailDto verifyEmailDto)
    {
        var result = await authService.VerifyEmailAsync(verifyEmailDto);
        return Ok(result);
    }

    [HttpPost("resend-verification")]
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<EmailResponseDto>> ResendVerification([FromBody] ResendVerificationDto resendDto)
    {
        var result = await authService.ResendVerificationEmailAsync(resendDto);

        // Return appropriate status code based on result
        if (!result.Success)
        {
            if (result.Message.Contains("no encontrado", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result);
            }
            if (result.Message.Contains("ya ha sido verificado", StringComparison.OrdinalIgnoreCase) ||
                result.Message.Contains("ya verificado", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(result);
            }
            // Email sending failed - Service Unavailable
            return StatusCode(503, result);
        }

        return Ok(result);
    }

    [HttpPost("forgot-password")]
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<EmailResponseDto>> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        var result = await authService.ForgotPasswordAsync(forgotPasswordDto);

        // ForgotPassword always returns success for security (even if user not found)
        // But if email sending fails, return 503
        if (!result.Success)
        {
            return StatusCode(503, result);
        }

        return Ok(result);
    }

    [HttpPost("reset-password")]
    [EnableRateLimiting("AuthPolicy")]
    public async Task<ActionResult<EmailResponseDto>> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var result = await authService.ResetPasswordAsync(resetPasswordDto);
        return Ok(result);
    }

    [HttpPost("login-twofa")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponseDto>> VerifyTwoFactor([FromBody] VerifyTwoFactorDto verifyTwoFactorDto)
    {
        var result = await authService.VerifyTwoFactorAsync(verifyTwoFactorDto);
        return Ok(result);
    }

    [Authorize]
    [HttpPost("change-twofa-status")]
    public async Task<ActionResult<AuthResponseDto>> ChangeTwoFactor([FromBody] ChangeTwoFactorDto changeTwoFactorDto)
    {
        var userId = User.FindFirst("sub")?.Value ??
                     User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new AuthResponseDto
            {
                Success = false,
                Message = "Not authenticated"
            });
        }

        Console.WriteLine($"User {userId} is changing 2FA status to: {changeTwoFactorDto.EnableTwoFactor}");

        var result = await authService.ChangeTwoFactorStatusByIdAsync(userId, changeTwoFactorDto);
        return Ok(result);
    }
}