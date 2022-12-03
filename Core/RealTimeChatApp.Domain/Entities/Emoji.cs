

namespace RealTimeChatApp.Domain.Entities;

public class Emoji :BaseEntity<Guid>
{
    public string Code { get; set; } = null!;
    public string? Name { get; set; }
}
