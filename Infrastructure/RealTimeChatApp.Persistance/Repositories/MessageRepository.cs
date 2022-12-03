

namespace RealTimeChatApp.Persistance.Repositories
{
    public class MessageRepository : EfRepository<Message, Guid>, IMessageRepository
    {
        public MessageRepository(RealTimeChatAppDbContext context) : base(context)
        {
        }
    }
}
