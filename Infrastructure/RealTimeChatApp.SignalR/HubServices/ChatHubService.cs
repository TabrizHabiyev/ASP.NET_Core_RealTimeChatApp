using Microsoft.AspNetCore.SignalR;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs;
using SignalRSwaggerGen.Attributes;

namespace RealTimeChatApp.SignalR.HubServices
{
    public class ChatHubService : Hub<IChatHubService> , IChatHubService
    {
        readonly IHubContext<Hub<IChatHubService>> _hubContext;
        public ChatHubService(IHubContext<Hub<IChatHubService>> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }
        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task SendMessage(ChatResponseMessageDto messageDto)
        {
            await _hubContext.Clients.Group(messageDto.ChatId.ToString()).SendAsync("ReceiveMessage", messageDto);
        }
        public async Task UpdateMessage(ChatResponseMessageDto messageDto)
        {
            await _hubContext.Clients.Group(messageDto.ChatId.ToString()).SendAsync("UpdateMessage", messageDto);
        }
        public async Task DeleteMessage(Guid messageId)
        {
            await _hubContext.Clients.All.SendAsync("DeleteMessage", messageId);
        }
    }
}
