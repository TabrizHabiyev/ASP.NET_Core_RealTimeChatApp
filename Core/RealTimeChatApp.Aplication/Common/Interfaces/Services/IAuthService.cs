using RealTimeChatApp.Application.DTOs.User;

namespace RealTimeChatApp.Application.Common.Interfaces.Services;

public interface  IAuthService
{
    Task<DTOs.Token> LoginAsync(LoginUserRequestDto loginUserRequestDto);
    Task<bool> PasswordResetdByEmailAsync(ResetUserPasswordRequestDto RequestDto);
    Task<bool> ConfirmEmail(ConfirmUserEmailRequestDto RequestDto);
    Task<bool> UpdateUserPassword(UpdateUserPasswordRequestDto RequestDto);
}
