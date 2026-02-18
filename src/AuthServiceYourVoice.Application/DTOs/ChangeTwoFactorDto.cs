using System.ComponentModel.DataAnnotations;

namespace AuthServiceYourVoice.Application.DTOs;

public class ChangeTwoFactorDto
{
    [Required]
    public bool EnableTwoFactor { get; set; }
}