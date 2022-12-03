using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Token;
using RealTimeChatApp.Application.Repositories;
using RealTimeChatApp.Application.UnitOfWork;
using RealTimeChatApp.Domain.Entities;
using RealTimeChatApp.Infrastructure.Services.Email;
using RealTimeChatApp.Infrastructure.Services.Token;
using RealTimeChatApp.Persistance.Contexts;
using RealTimeChatApp.Persistance.Repositories;
using RealTimeChatApp.Persistance.Services;

namespace RealTimeChatApp.Persistance.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly RealTimeChatAppDbContext _realTimeChatAppDbContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configration;
    public ITokenHandler TokenHandler { get; set; }
    public IEmailSenderService EmailSenderService { get; set; }
    public IUserService UserService { get; set; }
    public IAuthService AuthService { get; set; }

    private IMessageRepository _messageRepository;

    private IChatRepository _chatRepository;

    public UnitOfWork(
       RealTimeChatAppDbContext realTimeChatAppDbContext,
       IConfiguration configuration,
       UserManager<User> userManager,
       SignInManager<User> signInManager
     )
    {
        _realTimeChatAppDbContext = realTimeChatAppDbContext;
        _configration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        TokenHandler = new TokenHadler(_configration);
        EmailSenderService = new EmailSenderService(_configration);
        UserService = new UserService(_userManager,EmailSenderService);
        AuthService = new AuthService(_userManager, _signInManager, TokenHandler, EmailSenderService);
    }

    public IMessageRepository MessageRepository => _messageRepository ?? (_messageRepository = new MessageRepository(_realTimeChatAppDbContext));

    public IChatRepository ChatRepository => _chatRepository ?? (_chatRepository = new ChatRepository(_realTimeChatAppDbContext));


    public async Task Commit()
    {
        await _realTimeChatAppDbContext.SaveChangesAsync();
    }
}
