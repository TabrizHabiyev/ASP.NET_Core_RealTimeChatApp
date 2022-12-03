

namespace RealTimeChatApp.Application.DTOs;

public class ChatResponseDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ChatType Type { get; set; }
    public List<ChatMessageResponseDto>? ChatMessages { get; set; }
}
