
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Persistance.Contexts;

namespace RealTimeChatApp.Persistance.Repositories
{
    public class MessageRepository : EfRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(RealTimeChatAppDbContext context) : base(context)
        {
        }
    }
}
