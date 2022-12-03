
namespace RealTimeChatApp.Application.DTOs.Chat;

public class ChatMessageDto
{
    public int Id { get; set; }
    public string? Text { get; set; }
    public bool IsImage { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime Date { get; set; }
    public ChatUserDto User { get; set; } = null!;
    public ChatDto Chat { get; set; } = null!;
}
