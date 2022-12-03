
namespace RealTimeChatApp.Application.Common.Interfaces.Services;
public interface IChatService
{
    public Task<List<ChatAllResponseDto>> GetPrivateChats(Guid userId);
    public Task<List<ChatAllResponseDto>> GetUserJoinededRoom(Guid userId);
    public Task<List<ChatAllResponseDto>> GetRoomsAsync(Guid userId);
    public Task<ChatResponseDto> GetChatAsync(Guid id, Guid userId);
    public Task<Guid> CreatePrivateChat(Guid rootId, Guid targetId);
    public Task CreateRoom(string name, Guid userId);
    public Task<bool> DeleteChatAsync(Guid id, Guid userId);
    public Task<MessageResponseMessageDto> GetMessagesIdAsync(Guid id);
    public Task AddReactionAsync(ChatReactionDto reactionDto);
    public Task RemoveReactionAsync(Guid messageId, Guid userId);
    public Task<MessageResponseMessageDto> CreateMessageAsync(CreateMessageDto createMessage, Guid userId);
    public Task<MessageResponseMessageDto> UpdateMessageAsync(CreateMessageDto createMessage, Guid userId, Guid messageId);
    public Task JoinRoom(ChatJoinRoomDto chatJoinRoomDto);
    public Task<bool> DeleteMessageAsync(Guid id, Guid userId);
}
