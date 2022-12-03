using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs.Chat;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Persistance.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IChatHubService _chatHubService;

        public ChatService(IUnitOfWork unitOfWork, IMapper mapper, IChatHubService chatHubService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _chatHubService = chatHubService;
        }

        public async Task<ChatDto> GetChatByIdAsync(Guid id)
        {
            var chat = await _unitOfWork.ChatRepository.Get(id);
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<List<ChatDto>> GetAllChatsAsync()
        {
            var chats = await _unitOfWork.ChatRepository.GetAll().ToListAsync();
            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task<ChatDto> CreateChatAsync(ChatDto chatDto)
        {
            var chat = _mapper.Map<Chat>(chatDto);
            await _unitOfWork.ChatRepository.Insert(chat);
            await _unitOfWork.Commit();
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task<ChatDto> UpdateChatAsync(ChatDto chatDto)
        {
            var chat = _mapper.Map<Chat>(chatDto);
            await _unitOfWork.ChatRepository.Update(chat);
            await _unitOfWork.Commit();
            return _mapper.Map<ChatDto>(chat);
        }

        public async Task DeleteChatAsync(Guid id)
        {
            var chat = await _unitOfWork.ChatRepository.Get(id);
            await _unitOfWork.ChatRepository.Delete(chat);
            await _unitOfWork.Commit();
        }

        public async Task<List<ChatMessageDto>> GetMessagesByChatIdAsync(Guid id)
        {
            var messages = await _unitOfWork.MessageRepository.GetBy(m => m.ChatId == id).ToListAsync();
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }

        public async Task<ChatMessageDto> CreateMessageAsync(ChatMessageDto ChatMessageDto)
        {
            var message = _mapper.Map<Message>(ChatMessageDto);
            await _unitOfWork.MessageRepository.Insert(message);
            await _unitOfWork.Commit();
            var result = _mapper.Map<ChatMessageDto>(message);
            await _chatHubService.SendMessage(result);
            return result;
        }

        public async Task<ChatMessageDto> UpdateMessageAsync(ChatMessageDto ChatMessageDto)
        {
            var message = _mapper.Map<Message>(ChatMessageDto);
            await _unitOfWork.MessageRepository.Update(message);
            await _unitOfWork.Commit();
            var result = _mapper.Map<ChatMessageDto>(message);
            await _chatHubService.UpdateMessage(result);
            return result;
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var message = await _unitOfWork.MessageRepository.Get(id);
            await _unitOfWork.MessageRepository.Delete(message);
            await _unitOfWork.Commit(); ;
            await _chatHubService.DeleteMessage(id);
        }
    }

}
