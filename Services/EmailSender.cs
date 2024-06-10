using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Swaed.Helpers.Email;
using System.Net.Mail;
using System.Text.Encodings.Web;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Swaed.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly Helpers.Email.MailSettings _emailSettings;

        public EmailSender(IOptions<Helpers.Email.MailSettings> mailSettings)
        {
            _emailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string callbackUrl)
        {
            var client = new SendGridClient(_emailSettings.SendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_emailSettings.Mail, _emailSettings.DisplayName),
                Subject = "Confirmez votre adresse e-mail",
                HtmlContent = $"Veuillez confirmer votre adresse e-mail en <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>cliquant ici</a>."
            };
            msg.AddTo(new EmailAddress(email));

            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception("Failed to send email");
            }
        }

        public async Task SendWelcomeEmailAsync(IdentityUser request)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Views", "Home", "EmailMessage.cshtml");
            string mailText = await File.ReadAllTextAsync(filePath);
            mailText = mailText.Replace("[username]", request.UserName).Replace("[email]", request.Email);

            var client = new SendGridClient(_emailSettings.SendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_emailSettings.Mail, _emailSettings.DisplayName),
                Subject = $"Welcome {request.UserName}",
                HtmlContent = mailText
            };
            msg.AddTo(new EmailAddress(request.Email));

            var response = await client.SendEmailAsync(msg);
            if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception("Failed to send email");
            }
        }
    }

    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string callbackUrl);
        Task SendWelcomeEmailAsync(IdentityUser request);
    }
}
