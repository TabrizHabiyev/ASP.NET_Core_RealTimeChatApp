using RealTimeChatApp.Application.DTOs.User;

namespace RealTimeChatApp.Application.Common.Interfaces.Services;

public interface IUserService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
}
