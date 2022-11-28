using Microsoft.AspNetCore.Identity;

namespace RealTimeChatApp.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public User() : base()
    {
        Chats = new List<ChatUser>();
    }
    public ICollection<ChatUser> Chats { get; set; }
}
