using RealTimeChatApp.Domain.Entities.Common;

namespace RealTimeChatApp.Domain.Entities;

public class Reaction:BaseEntity<Guid>
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = null!;
    public Guid EmojiId { get; set; }
    public Emoji Emoji { get; set; } = null!;
}
