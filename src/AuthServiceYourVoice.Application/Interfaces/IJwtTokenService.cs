
using AuthServiceYourVoice.Domain.Entities;

namespace AuthServiceYourVoice.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(User user);
    string GenerateTemporaryTwoFactorToken(User user);
}