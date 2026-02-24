namespace AuthServiceYourVoice.Application.DTOs;

public class AuthResponseDto
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public bool RequiresTwoFactor { get; set; } = false;
    public string Token { get; set; } = string.Empty;
    public UserDetailsDto UserDetails { get; set; } = new();
    public DateTime ExpiresAt { get; set; }
}