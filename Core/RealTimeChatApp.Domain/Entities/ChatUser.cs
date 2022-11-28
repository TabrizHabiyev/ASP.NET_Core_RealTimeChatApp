using RealTimeChatApp.Domain.Enums;


namespace RealTimeChatApp.Domain.Entities;

public class ChatUser
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
    public UserRole Role { get; set; }
}
