

using RealTimeChatApp.Domain.Entities.Common;

namespace RealTimeChatApp.Domain.Entities;

public class Message : BaseEntity<Guid>
{
        public string? Text { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEdited { get; set; }
        public bool IsAttachment { get; set; }
        public string? AttachmentUrl { get; set; }
        public Guid? ReplyToMessageId { get; set; }
        public Message? ReplyToMessage { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
 }
