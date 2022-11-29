using Microsoft.AspNetCore.Identity;
using RealTimeChatApp.Application.Common.Interfaces.Services;
using RealTimeChatApp.Application.DTOs.User;
using RealTimeChatApp.Domain.Entities;
using System.Web;

namespace RealTimeChatApp.Persistance.Services;

public class UserService : IUserService
{
    readonly UserManager<User> _userManager;
    private readonly IEmailSenderService _emailSender;
    public UserService(UserManager<User> userManager, IEmailSenderService emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
    {
        IdentityResult result = await _userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid(),
            UserName = model.UserName,
            Email = model.Email
        }, model.Password) ;
          
        CreateUserResponseDto response = new() { Success = result.Succeeded };
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            var confirmLink = $"https://localhost:7029/api/Auth/ConfirimEmail/{user.Id}/" +
                HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

            bool sendEmailConfirim = await _emailSender.SendEmailForConfirimEmail(model.Email, confirmLink);

            if (sendEmailConfirim)
            {
                response.Message = "User created successfully";
            }
            else
            {
                response.Message = "There was an error creating the user, please try again later";
                response.Success = false;
                await _userManager.DeleteAsync(user);
            }
        }
        else
        {
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - ${error.Description}\n";
            }
        }

        return response;
    }
}
