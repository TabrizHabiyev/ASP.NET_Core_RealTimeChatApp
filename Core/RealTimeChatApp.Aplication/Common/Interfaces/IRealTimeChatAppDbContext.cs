

using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.Common.Interfaces;

public interface IRealTimeChatAppDbContext
{
    DbSet<Chat> Chats { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<ChatUser> ChatUsers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
