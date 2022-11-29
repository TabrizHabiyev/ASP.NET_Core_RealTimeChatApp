using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Token;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Domain.Entities;
using System.Web;

namespace RealTimeChatApp.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailSenderService _emailHandler;
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IUserService userService, IEmailSenderService emailHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _userService = userService;
        _emailHandler = emailHandler;
    }

    public async Task<Token> LoginAsync(string email, string password)
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new Exception("User not found");

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);

            Token token = _tokenHandler.CreateAccessToken(3, user, roles);

            return token;
        }
        throw new Exception("User not found");
    }


    public async Task<(bool, bool)> PasswordResetdByEmailAsync(string email)
    {
        User? user = await _userManager.FindByEmailAsync(email);
        if (!await _userManager.IsEmailConfirmedAsync(user)) return (false, false);


        if (user != null)
        {

            string resetToken = $"https://localhost:7029/api/Auth/UpdatePassword/{user.Id}/" + HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
            bool result = await _emailHandler.SendEmailForResetPassword(email, resetToken);

            return (result, true);
        }
        else
            return (false, false);
    }


    public async Task<bool> UpdateUserPassword(string userId, string token, string password)
    {
        User? user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), password);
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return result.Succeeded;
        }
        else return false;
    }

    public async Task<bool> ConfirmEmail(string userId, string token)
    {
        User? user = await _userManager.FindByIdAsync(userId);

        if (user == null) return false;

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(token));
        if (result.Succeeded)
        {
            return result.Succeeded;
        }
        else return false;
    }
}
