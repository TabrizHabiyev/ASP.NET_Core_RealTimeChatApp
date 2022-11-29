namespace RealTimeChatApp.Application.Common.Interfaces.Services;

public interface IEmailSenderService
{
    Task<bool> SendEmailForResetPassword(string email, string resetToken);
    Task<bool> SendEmailForConfirimEmail(string email, string resetToken);
}
