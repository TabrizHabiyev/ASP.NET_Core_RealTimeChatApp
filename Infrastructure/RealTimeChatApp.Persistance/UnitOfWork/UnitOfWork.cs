
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Persistance.Contexts;

namespace RealTimeChatApp.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly RealTimeChatAppDbContext _realTimeChatAppDbContext;

    public UnitOfWork(RealTimeChatAppDbContext realTimeChatAppDbContext)
    {
        _realTimeChatAppDbContext = realTimeChatAppDbContext;
    }

    public async Task Commit()
    {
        await _realTimeChatAppDbContext.SaveChangesAsync();
    }
}
