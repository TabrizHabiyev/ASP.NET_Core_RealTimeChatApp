using RealTimeChatApp.Application.DTOs.Chat;

namespace RealTimeChatApp.Application.Common.Interfaces.Services;
public interface IChatService
{
    public Task<ChatDto> GetChatByIdAsync(Guid id);
    public Task<List<ChatDto>> GetAllChatsAsync();
    public Task<ChatDto> CreateChatAsync(ChatDto chatDto);
    public Task<ChatDto> UpdateChatAsync(ChatDto chatDto);
    public Task DeleteChatAsync(Guid id);
    public Task<List<ChatMessageDto>> GetMessagesByChatIdAsync(Guid id);
    public Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto messageDto);
    public Task<ChatMessageDto> UpdateMessageAsync(ChatMessageDto messageDto);
    public Task DeleteMessageAsync(Guid id);
}
