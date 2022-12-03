

using RealTimeChatApp.Domain.Entities;

namespace RealTimeChatApp.Application.Repositories;

public interface IMessageRepository : IRepository<Message, Guid>
{

}
