using RealTimeChatApp.Application.DTOs;
namespace RealTimeChatApp.Application.Common.Interfaces.Services;

    public interface IChatHubService
    {
        public Task JoinRoom(string roomId);
        public Task LeaveRoom(string roomId);
        public Task SendMessage(ChatResponseMessageDto messageDto);
        public Task UpdateMessage(ChatResponseMessageDto messageDto);
        public Task DeleteMessage(Guid messageId);
    }

