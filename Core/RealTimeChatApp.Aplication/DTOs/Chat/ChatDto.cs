

namespace RealTimeChatApp.Application.DTOs.Chat;

public class ChatDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<ChatUserDto>? Users { get; set; }
}
