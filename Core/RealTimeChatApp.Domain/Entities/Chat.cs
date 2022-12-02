using RealTimeChatApp.Domain.Entities.Common;
using RealTimeChatApp.Domain.Enums;

namespace RealTimeChatApp.Domain.Entities;

public class Chat: BaseEntity<Guid>
{
    public Chat()
    {
        Messages = new List<Message>();
        Users = new List<ChatUser>();
    }
    public override Guid Id { get; set; }

    public string Name { get; set; } = null!;
    public ChatType Type { get; set; }
    public ICollection<Message> Messages { get; set; }
    public ICollection<ChatUser> Users { get; set; }
}