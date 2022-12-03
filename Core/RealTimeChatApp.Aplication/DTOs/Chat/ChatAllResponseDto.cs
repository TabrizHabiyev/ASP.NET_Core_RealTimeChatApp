using RealTimeChatApp.Domain.Enums;

namespace RealTimeChatApp.Application.DTOs
{
    public class ChatAllResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ChatType Type { get; set; }
    }
}
