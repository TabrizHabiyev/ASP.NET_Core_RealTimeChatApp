using RealTimeChatApp.Application.DTOs.Chat;

namespace RealTimeChatApp.Application.Common.Interfaces.Services
{
    public interface IChatHubService
    {
       public Task SendMessage(ChatMessageDto messageDto);
       public Task UpdateMessage(ChatMessageDto messageDto);
       public Task DeleteMessage(Guid messageId);
    }
}
