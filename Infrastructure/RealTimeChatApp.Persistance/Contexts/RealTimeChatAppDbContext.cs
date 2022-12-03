using RealTimeChatApp.Persistance.Configurations.Filters;

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
        builder.UseSoftDelete();
        base.OnModelCreating(builder);
    }
}



