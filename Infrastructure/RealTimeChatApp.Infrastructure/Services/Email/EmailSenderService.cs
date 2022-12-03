

namespace RealTimeChatApp.Infrastructure.Services.Email;

public class EmailSenderService : IEmailSenderService
{
   private readonly IConfiguration _configuration;
   public EmailSenderService(IConfiguration configuration)
   {
       _configuration = configuration;
   }
    public async Task<bool> SendEmailForConfirimEmail(string email, string confirmToken)
    {
        var client = new SendGridClient(_configuration["EmailConfiguration:ApiKey"]);
        var from = new EmailAddress(_configuration["EmailConfiguration:From"], "Tabriz Habiyev");
        var subject = "Confirim Email";
        var to = new EmailAddress(email, "Tabriz Habiyev");
        var plainTextContent = "Confirim email";
        var htmlContent = $"<strong>Click to link to confirim email</strong> <a href='{confirmToken}'>Click</a>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        try
        {
            var res= await client.SendEmailAsync(msg);
            if (res.IsSuccessStatusCode == true)
            {
                return true;
            }
            else return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> SendEmailForResetPassword(string email, string resetToken)
    {
        var client = new SendGridClient(_configuration["EmailConfiguration:ApiKey"]);
        var from = new EmailAddress(_configuration["EmailConfiguration:From"], "Tabriz Habiyev");
        var subject = "Reset Password";
        var to = new EmailAddress(email, "Tabriz Habiyev");
        var plainTextContent = "Reset Password";
        var htmlContent = $"<strong>Click to link to reset password</strong> <a href='{resetToken}'>Click</a>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        try
        {
            var res= await client.SendEmailAsync(msg);
            if (res.IsSuccessStatusCode == true)
            {
                return true;
            }
            else return false;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}
