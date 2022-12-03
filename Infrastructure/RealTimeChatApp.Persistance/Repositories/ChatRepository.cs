using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Persistance.Contexts;

namespace RealTimeChatApp.Persistance.Repositories;

public class ChatRepository : EfRepository<Chat, Guid>, IChatRepository
{
    public ChatRepository(RealTimeChatAppDbContext context) : base(context)
    {
    }
}
