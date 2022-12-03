using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Aplication.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Domain.Enums;
using GlobalEx =RealTimeChatApp.Domain.ExceptionModels.Common;
namespace RealTimeChatApp.Persistance.Services
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IChatHubService _chatHubService;
        private readonly ICloidinaryFileServices _cloidinaryFileServices;
        UserManager<User> _userManager;
        public ChatService(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IChatHubService chatHubService, 
        ICloidinaryFileServices cloidinaryFileServices,
        UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _chatHubService = chatHubService;
            _cloidinaryFileServices = cloidinaryFileServices;
            _userManager = userManager;
        }

        public async Task<List<ChatDto>> GetPrivateChats(Guid userId)
        {
            var chats = await _unitOfWork.ChatRepository.GetAllIncluding(x => x.Users.Any(x => x.UserId == userId), x => x.Users)
            .Where(x => x.Type == ChatType.Private).ToListAsync();
            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task<List<ChatDto>> GetChats(Guid userId)
        {
            var chats = await _unitOfWork.ChatRepository.GetAllIncluding(x => x.Users)
            .Where(x => !x.Users.Any(x => x.UserId == userId)).ToListAsync();
            List<ChatDto> chatsDto = _mapper.Map<List<ChatDto>>(chats);
            return _mapper.Map<List<ChatDto>>(chats);
        }

        public async Task<ChatDto> GetChatAsync(Guid id)
        {
          var chat = await _unitOfWork.ChatRepository.GetSingleIncluding(x => x.Id == id, x => x.Messages);
          var chatDto = _mapper.Map<ChatDto>(chat);
          chatDto.Users = chat.Users.Select(x => new ChatUserDto{ Id = x.UserId, UserName = x.User.UserName }).ToList();
          return chatDto;
        }
        

        public async Task<Guid> CreatePrivateChat(Guid rootId, Guid targetId)
        {
            bool chatExists = await _unitOfWork.ChatRepository.GetSingleIncluding(
                x => x.Users.Any(x => x.UserId == rootId) && x.Users.Any(x => x.UserId == targetId),
                x => x.Users) != null;

            if (chatExists){
                throw new GlobalEx.BadRequestException("Chat already exists");
            }else{
            var chat = new Chat
            {
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser
            {
                UserId = targetId
            });

            chat.Users.Add(new ChatUser
            {
                UserId = rootId
            });
            await _unitOfWork.ChatRepository.Insert(chat);
            await _unitOfWork.Commit();
            return chat.Id;
            }
        }

        public async Task CreateRoom(string name, Guid userId)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            chat.Users.Add(new ChatUser
            {
                UserId = userId,
                Role = UserRole.Admin
            });
            await _unitOfWork.ChatRepository.Insert(chat);
            await _unitOfWork.Commit();
        }

        public async Task<bool> DeleteChatAsync(Guid id, Guid userId)
        {
           var user = _userManager.Users.FirstOrDefault(x => x.Id == userId);
           if(user == null)
           {
               throw new GlobalEx.NotFoundException("User Not Found");
           }
           var chatUsers = await _unitOfWork.ChatRepository.GetBy(x => x.Users.Any(x => x.UserId == userId)).FirstOrDefaultAsync();
           
            if(chatUsers == null)
            {
               throw new GlobalEx.NotFoundException("Chat Not Found");
            }
            if(chatUsers.Users.Any(x => x.UserId == userId && x.Role == UserRole.Admin))
            {
               var chat = await _unitOfWork.ChatRepository.Get(id);
               await _unitOfWork.Commit();
               return true;
            }
            else
            {
                throw new GlobalEx.ForbiddenException("You are not allowed to delete this chat");
            }
        }

        public async Task<ChatResponseMessageDto> GetMessagesIdAsync(Guid id)
        {
            var messages = await _unitOfWork.MessageRepository.Get(id);
            var rection = await _unitOfWork.ReactionRepository.GetSingleIncluding(x => x.MessageId == id, x => x.Emoji);
            var ResponseMessage = _mapper.Map<ChatResponseMessageDto>(messages);
            ResponseMessage.SenderUser = _mapper.Map<ChatUserDto>(messages.User);
            return messages == null ? throw new GlobalEx.NotFoundException("Messages Not Found") : 
            ResponseMessage;
        }

     
        public async Task AddReactionAsync(ChatReactionDto reactionDto)
        {
           await _unitOfWork.ReactionRepository.Insert(_mapper.Map<Reaction>(reactionDto));
           await _unitOfWork.Commit();
        }

        public async Task RemoveReactionAsync(Guid messageId, Guid userId)
        {
            var reaction = await _unitOfWork.ReactionRepository.GetBy(x => x.MessageId == messageId && x.Message.UserId == userId).FirstOrDefaultAsync();
            if(reaction == null)
            {
                throw new GlobalEx.NotFoundException("Reaction Not Found");
            }
            await _unitOfWork.ReactionRepository.Delete(reaction);
            await _unitOfWork.Commit();
        }

        public async Task<ChatResponseMessageDto> CreateMessageAsync(CreateMessageDto createMessage , Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var chat = await _unitOfWork.ChatRepository.GetSingleIncluding(x => x.Id == createMessage.ChatId, x => x.Users);
            if(chat == null)
            {
                throw new GlobalEx.NotFoundException("Chat Not Found");
            }
            if(!chat.Users.Any(x => x.UserId == userId))
            {
                throw new GlobalEx.ForbiddenException("You are not allowed to send message to this chat");
            }
            
            if (createMessage.File != null)
            {
                var uploadResult = await _cloidinaryFileServices.AddImageAsync(createMessage.File);
                createMessage.AttachmentUrl = uploadResult.Url.ToString();
            }
            Message? message = _mapper.Map<Message>(createMessage);
            message.User = user;
            message.Chat = chat;
            message.IsAttachment = createMessage.AttachmentUrl != null ? true : false;
            await _unitOfWork.MessageRepository.Insert(message);
            await _unitOfWork.Commit();
            var chatMessageDto = _mapper.Map<ChatResponseMessageDto>(message);
            await _chatHubService.SendMessage(chatMessageDto);
            return chatMessageDto;
         }

        public async Task<ChatResponseMessageDto> UpdateMessageAsync(CreateMessageDto createMessage ,Guid userId,Guid messageId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var chat = await _unitOfWork.ChatRepository.GetSingleIncluding(x => x.Id == createMessage.ChatId, x => x.Users);
            if(chat == null)
            {
                throw new GlobalEx.NotFoundException("Chat Not Found");
            }
            if(!chat.Users.Any(x => x.UserId == userId))
            {
                throw new GlobalEx.ForbiddenException("You are not allowed to send message to this chat");
            }
            var message = await _unitOfWork.MessageRepository.GetSingleIncluding(x => x.Id == messageId, x => x.User);
            if(message == null)
            {
                throw new GlobalEx.NotFoundException("Message Not Found");
            }
            if(message?.User?.Id != userId)
            {
                throw new GlobalEx.ForbiddenException("You are not allowed to update this message");
            }
            if (createMessage.File != null)
            {
                var uploadResult = await _cloidinaryFileServices.AddImageAsync(createMessage.File);
                createMessage.AttachmentUrl = uploadResult.Url.ToString();
            }
            message = _mapper.Map<Message>(createMessage);
            message.User = user;
            message.Chat = chat;
            message.IsEdited = true;
            message.IsAttachment = createMessage.AttachmentUrl != null ? true : false;
            await _unitOfWork.MessageRepository.Update(message);
            await _unitOfWork.Commit();
            var chatMessageDto = _mapper.Map<ChatResponseMessageDto>(message);
            await _chatHubService.SendMessage(chatMessageDto);
            return chatMessageDto;
        }

        public async Task JoinRoom(ChatJoinRoomDto chatJoinRoomDto)
        {
            var chat = await _unitOfWork.ChatRepository.Get(chatJoinRoomDto.ChatId);
            if (chat == null)
            {
                throw new GlobalEx.NotFoundException("Chat Not Found");
            }
            if (chat.Type != ChatType.Room)
            {
                throw new GlobalEx.BadRequestException("Chat is not a room");
            }
            var user = await _userManager.FindByIdAsync(chatJoinRoomDto.UserId.ToString());
            if (user == null)
            {
                throw new GlobalEx.NotFoundException("User Not Found");
            }
            if (chat.Users.Any(x => x.UserId == user.Id))
            {
                throw new GlobalEx.BadRequestException("User already joined this chat");
            }
            chat.Users.Add(new ChatUser
            {
                UserId = user.Id
            });
            await _unitOfWork.Commit();
        }

        public async Task<bool> DeleteMessageAsync(Guid id, Guid userId)
        {
            var message = await _unitOfWork.MessageRepository.Get(id);
            if(message == null)
            {
                throw new GlobalEx.NotFoundException("Message Not Found");
            }
            if(message.UserId != userId)
            {
                throw new GlobalEx.ForbiddenException("You are not allowed to delete this message");
            }
            message.IsDeleted = true;
            await _unitOfWork.Commit();
            return true;
        }
    }

}
