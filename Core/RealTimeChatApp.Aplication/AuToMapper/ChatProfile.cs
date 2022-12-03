using AutoMapper;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Domain.Entities.Common;
using RealTimeChatApp.Domain.Enums;

namespace RealTimeChatApp.Application.AuToMapper;

    public class ChatProfile : Profile
    {
    public ChatProfile()
        {
               CreateMap<Chat, ChatDto>().ReverseMap();

               CreateMap<Message, ChatResponseMessageDto>();

               CreateMap<CreateMessageDto, Message>();


               CreateMap<User, ChatUserDto>().ReverseMap();
        }
}
