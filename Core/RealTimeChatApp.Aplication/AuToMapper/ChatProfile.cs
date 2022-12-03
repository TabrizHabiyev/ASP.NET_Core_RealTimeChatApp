using AutoMapper;
using RealTimeChatApp.Application.DTOs.Chat;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.AuToMapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<Chat, ChatDto>()
                .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Select(uc => uc.User)));
            CreateMap<User, ChatUserDto>();
            CreateMap<Message, ChatMessageDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        }
    }
}
