using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RealTimeChatApp.Persistance.Contexts;

    
public class RealTimeChatAppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IRealTimeChatAppDbContext
{
    public RealTimeChatAppDbContext(DbContextOptions<RealTimeChatAppDbContext> options):base(options){}

    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Message> Messages => Set<Message>();
    public DbSet<ChatUser> ChatUsers => Set<ChatUser>();
    public DbSet<Reaction> Reactions => Set<Reaction>();
    public DbSet<Emoji> Emojis => Set<Emoji>();
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
