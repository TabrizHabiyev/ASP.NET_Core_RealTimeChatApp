using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Persistance.Contexts;

namespace RealTimeChatApp.Persistance.Repositories;

public class ReactionRepository : EfRepository<Reaction, Guid>, IReactionRepository
{
    public ReactionRepository(RealTimeChatAppDbContext context) : base(context)
    {
    }
}
