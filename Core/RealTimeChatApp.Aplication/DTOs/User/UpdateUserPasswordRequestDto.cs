namespace RealTimeChatApp.Application.DTOs.User;

public class UpdateUserPasswordRequestDto
{
    public string userId { get; set; } = null!;
    public string token { get; set; } = null!;
    public string password { get; set; } = null!;
}
