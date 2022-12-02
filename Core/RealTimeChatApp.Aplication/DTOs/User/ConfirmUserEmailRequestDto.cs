

namespace RealTimeChatApp.Application.DTOs.User;

public class ConfirmUserEmailRequestDto
{
    public string userId { get; set; } = null!;
    public string token { get; set; } = null!;
}
