using System.ComponentModel.DataAnnotations;

namespace AuthServiceYourVoice.Application.DTOs.Email;

public class VerifyEmailDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
}