using RealTimeChatApp.Domain.Enums;


namespace RealTimeChatApp.Domain.Entities;

public class ChatUser
{
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public UserRole Role { get; set; }
}
