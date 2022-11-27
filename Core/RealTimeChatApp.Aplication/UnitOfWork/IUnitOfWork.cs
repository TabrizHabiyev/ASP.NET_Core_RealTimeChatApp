
namespace RealTimeChatApp.Application.UnitOfWork;

public interface IUnitOfWork
{
    public Task Commit();
}
