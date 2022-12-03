

namespace RealTimeChatApp.Application.AuToMapper;

public class ChatProfile : Profile
{
    public ChatProfile()
    {
        CreateMap<Chat, ChatAllResponseDto>();

        CreateMap<Chat, ChatResponseDto>()
            .ForMember(dest => dest.ChatMessages, opt => opt.MapFrom(src => src.Messages))
            .ReverseMap();
        CreateMap<Message, ChatMessageResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ReverseMap();

        CreateMap<Message, MessageResponseMessageDto>()
            .ForMember(dest => dest.SenderUser, opt => opt.MapFrom(src => src.User));

        CreateMap<CreateMessageDto, Message>();

        CreateMap<User, ChatUserDto>().ReverseMap();
    }
}
