using RealTimeChatApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.Domain.Entities;

public class Message : BaseEntity<Guid>
{
        public string? Text { get; set; }
        bool IsDeleted { get; set; }
        bool IsEdited { get; set; }
        bool IsImage { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
