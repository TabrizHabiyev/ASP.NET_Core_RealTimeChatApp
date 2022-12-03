using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Persistance.Repositories;

public class ChatRepository : EfRepository<Chat, Guid>, IChatRepository
{
}
