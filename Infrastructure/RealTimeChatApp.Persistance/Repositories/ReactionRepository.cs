

namespace RealTimeChatApp.Persistance.Repositories;

public class ReactionRepository : EfRepository<Reaction, Guid>, IReactionRepository
{
    public ReactionRepository(RealTimeChatAppDbContext context) : base(context)
    {
    }
}
