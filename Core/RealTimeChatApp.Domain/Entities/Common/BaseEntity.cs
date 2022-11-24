

namespace RealTimeChatApp.Domain.Entities.Common;

public class BaseEntity<TPrimarykey>
{
    public virtual TPrimarykey Id { get; set;}
}
