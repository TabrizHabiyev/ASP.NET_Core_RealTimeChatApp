using RealTimeChatApp.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.Domain.Entities;

public class Message : BaseEntity<Guid>
{
    public override Guid Id { get; set; }
    public string Name { get; set; }
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }
    public Guid ChatId { get; set; }
    public Chat Chat { get; set; }
}
