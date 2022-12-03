

using RealTimeChatApp.Domain;

namespace RealTimeChatApp.Persistance.Configurations;

internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("Chats");
    }
}

