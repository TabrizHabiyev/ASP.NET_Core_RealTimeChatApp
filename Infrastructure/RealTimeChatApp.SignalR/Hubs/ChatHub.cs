using Microsoft.AspNetCore.SignalR;


namespace RealTimeChatApp.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinToChat(Guid chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
        }
        public async Task LeaveFromChat(Guid chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
        }
    }
}
