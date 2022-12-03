

namespace RealTimeChatApp.Application.DTOs;

public class CreateMessageDto
{
    public Guid ChatId { get; set; }
    public string? Text { get; set; }
    public string? AttachmentUrl { get; set; }
    public IFormFile? File { get; set; }
}
