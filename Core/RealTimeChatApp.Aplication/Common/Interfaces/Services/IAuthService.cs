namespace RealTimeChatApp.Application.Common.Interfaces.Services;

public interface  IAuthService
{
    Task<DTOs.Token> LoginAsync(string email, string passwoed);
    Task<(bool mail, bool user)> PasswordResetdByEmailAsync(string email);
    Task<bool> ConfirmEmail(string userId, string token);
    Task<bool> UpdateUserPassword(string userId, string token, string password);
}
