using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs.Chat;
using RealTimeChatApp.SignalR.Hubs;

namespace RealTimeChatApp.SignalR.HubServices
{
    public class ChatHubService : IChatHubService
    {
        readonly IHubContext<ChatHub> _hubContext;
        public ChatHubService(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task SendMessage(ChatMessageDto messageDto)
        {
            await _hubContext.Clients.Group(messageDto.Chat.Id.ToString()).SendAsync("ReceiveMessage", messageDto);
        }
        public async Task UpdateMessage(ChatMessageDto messageDto)
        {
            await _hubContext.Clients.Group(messageDto.Chat.Id.ToString()).SendAsync("UpdateMessage", messageDto);
        }
        public async Task DeleteMessage(Guid messageId)
        {
            await _hubContext.Clients.All.SendAsync("DeleteMessage", messageId);
        }
    }
}
