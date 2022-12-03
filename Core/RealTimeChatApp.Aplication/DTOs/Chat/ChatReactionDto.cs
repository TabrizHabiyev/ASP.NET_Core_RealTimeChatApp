namespace RealTimeChatApp.Application.DTOs;

public class ChatReactionDto
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public Guid EmojiId { get; set; }
}
