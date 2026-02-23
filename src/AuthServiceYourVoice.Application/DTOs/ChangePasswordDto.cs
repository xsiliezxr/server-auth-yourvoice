using System.ComponentModel.DataAnnotations;

namespace AuthServiceYourVoice.Application.DTOs;

public class ChangePasswordDto
{

    [Required]
    [MinLength(8)]
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}