using System.ComponentModel.DataAnnotations;
namespace AuthServiceYourVoice.Domain.Entities;

public class UserSecurity
{
    [Key]
    [MaxLength(16)]
    public string Id { get; set; } = string.Empty;

    public Boolean IsTwoFactorEnabled { get; set; } = false;

    [MaxLength(8, ErrorMessage = "The code must be exactly 8 characters long.")]
    public string? TwoFactorCode { get; set; }
    public DateTime? TwoFactorCodeExpiration { get; set; }

    [Required]
    [MaxLength(16)] 
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; } = null!;
}