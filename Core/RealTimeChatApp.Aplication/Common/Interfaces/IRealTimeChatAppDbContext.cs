using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.Common.Interfaces;

public interface IRealTimeChatAppDbContext
{
    DbSet<Chat> Chats { get;}
    DbSet<Message> Messages { get;}
    DbSet<ChatUser> ChatUsers { get;}
    DbSet<Reaction> Reactions { get; }
    DbSet<Emoji> Emojis { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
