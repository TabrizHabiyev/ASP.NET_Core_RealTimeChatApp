using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.Repositories
{
    public interface IReactionRepository : IRepository<Reaction, Guid>
    {
    }
}
