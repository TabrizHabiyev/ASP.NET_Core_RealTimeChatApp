
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace RealTimeChatApp.Application.DTOs;


public class ChatResponseMessageDto
{
    public Guid Id { get; set; } 
    public Guid ChatId { get; set; }
    public string? Text { get; set; }
    public bool IsAttachment { get; set; }
    public bool IsEdited { get; set; }
    public string? AttachmentUrl { get; set; }
    public DateTime Timestamp { get; set; }
    public string? Reaction { get; set; }
    public ChatUserDto SenderUser { get; set; } = null!;
}




