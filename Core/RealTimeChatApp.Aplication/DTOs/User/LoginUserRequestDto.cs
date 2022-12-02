namespace RealTimeChatApp.Application.DTOs.User;

    public class LoginUserRequestDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
