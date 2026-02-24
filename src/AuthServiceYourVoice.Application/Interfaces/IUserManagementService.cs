using AuthServiceYourVoice.Application.DTOs;

namespace AuthServiceYourVoice.Application.Interfaces;

public interface IUserManagementService
{
    Task<UserResponseDto> UpdateUserRoleAsync(string userId, string roleName);
    Task<IReadOnlyList<string>> GetUserRolesAsync(string userId);
    Task<IReadOnlyList<UserResponseDto>> GetUsersByRoleAsync(string roleName);
    Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);

}