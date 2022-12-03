using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Persistance.Configurations;

internal class ChatUserConfiguration : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.HasKey(cu => new { cu.ChatId, cu.UserId });

        builder.HasOne(cu => cu.Chat)
            .WithMany(c => c.Users)
            .HasForeignKey(cu => cu.ChatId);

        builder.HasOne(cu => cu.User)
            .WithMany(u => u.Chats)
            .HasForeignKey(cu => cu.UserId);

        builder.ToTable("ChatUsers");
    }
}
