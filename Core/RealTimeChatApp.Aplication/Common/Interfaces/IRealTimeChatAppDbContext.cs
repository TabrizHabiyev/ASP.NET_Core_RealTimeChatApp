

using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.Common.Interfaces;

public interface IRealTimeChatAppDbContext
{
    DbSet<Chat> Chats { get;}
    DbSet<Message> Messages { get;}
    DbSet<ChatUser> ChatUsers { get;}
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
