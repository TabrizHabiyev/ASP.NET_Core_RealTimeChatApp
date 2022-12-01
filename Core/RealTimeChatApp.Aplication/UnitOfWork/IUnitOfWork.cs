
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Token;

namespace RealTimeChatApp.Application.UnitOfWork;

public interface IUnitOfWork
{
    public ITokenHandler TokenHandler { get; set; }
    public IEmailSenderService EmailSenderService { get; set; }
    public IUserService UserService { get; set; }
    public IAuthService AuthService { get; set; }
    public Task Commit();
}
