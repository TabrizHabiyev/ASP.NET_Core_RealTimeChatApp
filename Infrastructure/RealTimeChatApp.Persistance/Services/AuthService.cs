using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.Common.Interfaces.Token;
using RealTimeChatApp.Application.DTOs;
using RealTimeChatApp.Application.DTOs.User;
using RealTimeChatApp.Domain.Entities;
using System.Web;

namespace RealTimeChatApp.Persistance.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailSenderService _emailHandler;
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenHandler tokenHandler, IEmailSenderService emailHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
        _emailHandler = emailHandler;
    }

    public async Task<Token> LoginAsync(LoginUserRequestDto loginUserRequestDto)
    {
        User? user = await _userManager.FindByEmailAsync(loginUserRequestDto.Email);

        if (user == null) throw new Exception("User not found");

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginUserRequestDto.Password, false);
        if (result.Succeeded)
        {
            var roles = await _userManager.GetRolesAsync(user);

            Token token = _tokenHandler.CreateAccessToken(3, user, roles);

            return token;
        }
        else throw new Exception("Password is not correct");
    }


    public async Task<bool> PasswordResetdByEmailAsync(ResetUserPasswordRequestDto RequestDto)
    {
        User? user = await _userManager.FindByEmailAsync(RequestDto.Email);
        
        if (user != null)
        {
            
            string resetToken = $"https://localhost:7029/api/Auth/UpdatePassword/{user.Id}/" + HttpUtility.UrlEncode(await _userManager.GeneratePasswordResetTokenAsync(user));
            bool result = await _emailHandler.SendEmailForResetPassword(RequestDto.Email, resetToken);
            
            if (result)
                return true;
            else  throw new Exception("Email not sent");
        }
        else throw new Exception("User not found");
    }


    public async Task<bool> UpdateUserPassword(UpdateUserPasswordRequestDto RequestDto)
    {
        User? user = await _userManager.FindByIdAsync(RequestDto.userId);
        if (user == null) throw new Exception("User not found");
        IdentityResult result = await _userManager.ResetPasswordAsync(user, HttpUtility.UrlDecode(RequestDto.token), RequestDto.password);
        if (result.Succeeded)
        {
            await _userManager.UpdateSecurityStampAsync(user);
            return result.Succeeded;
        }
        else throw new Exception("Tken is not valid or expired");
    }

    public async Task<bool> ConfirmEmail(ConfirmUserEmailRequestDto RequestDto)
    {
        User? user = await _userManager.FindByIdAsync(RequestDto.userId);

        if (user == null) throw new Exception("User not found");

        IdentityResult result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(RequestDto.token));
        if (result.Succeeded)
        {
            return result.Succeeded;
        }
        else throw new Exception("Tken is not valid or expired");
    }
}
