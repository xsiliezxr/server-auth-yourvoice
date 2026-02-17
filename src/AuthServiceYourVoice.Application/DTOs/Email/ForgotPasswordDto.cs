using System.ComponentModel.DataAnnotations;

namespace AuthServiceYourVoice.Application.DTOs.Email;

public class ForgotPasswordDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}