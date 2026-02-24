using System.ComponentModel.DataAnnotations;
namespace AuthServiceYourVoice.Application.DTOs;

public class VerifyTwoFactorDto
{

    [Required]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "The code must be exactly 8 characters long.")]
    public string TwoFactorCode { get; set; } = string.Empty;
}