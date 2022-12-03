
namespace RealTimeChatApp.Application.DTOs;

public class ChatMessageResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    public string? Text { get; set; }
    public bool IsAttachment { get; set; }
    public bool IsEdited { get; set; }
    public string? AttachmentUrl { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Reaction { get; set; }
}
