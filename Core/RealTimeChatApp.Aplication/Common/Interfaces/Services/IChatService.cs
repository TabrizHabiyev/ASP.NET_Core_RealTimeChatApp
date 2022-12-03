using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Aplication.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Domain.Enums;

namespace RealTimeChatApp.Application.Common.Interfaces.Services;
public interface IChatService
{
    public Task<List<ChatDto>> GetPrivateChats(Guid userId);
    public Task<List<ChatDto>> GetChats(Guid userId);
    public Task<ChatDto> GetChatAsync(Guid id);
    public Task<Guid> CreatePrivateChat(Guid rootId, Guid targetId);
    public Task CreateRoom(string name, Guid userId);
    public Task<bool> DeleteChatAsync(Guid id, Guid userId);
    public Task<ChatResponseMessageDto> GetMessagesIdAsync(Guid id);
    public Task AddReactionAsync(ChatReactionDto reactionDto);
    public Task RemoveReactionAsync(Guid messageId, Guid userId);
    public Task<ChatResponseMessageDto> CreateMessageAsync(CreateMessageDto createMessage, Guid userId);
    public Task<ChatResponseMessageDto> UpdateMessageAsync(CreateMessageDto createMessage, Guid userId, Guid messageId);
    public Task JoinRoom(ChatJoinRoomDto chatJoinRoomDto);
    public Task<bool> DeleteMessageAsync(Guid id, Guid userId);
}
