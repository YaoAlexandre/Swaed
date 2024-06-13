using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Swaed.Helpers.Email;
using System.Net.Mail;
using System.Text.Encodings.Web;
using SendGrid.Helpers.Mail;
using SendGrid;
using Swaed.Models;
using System;
using System.Text;
using System.Net;

namespace Swaed.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly Helpers.Email.MailSettings _emailSettings;
        private readonly Helpers.Email.MailSettings _smtpSettings;

        public EmailSender(IOptions<Helpers.Email.MailSettings> mailSettings, IOptions<Helpers.Email.MailSettings> smtpSettings)
        {
            _emailSettings = mailSettings.Value;
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpSettings.Mail, _smtpSettings.Password);
                client.EnableSsl = true;

                var message = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.Mail),
                    Subject = subject,
                    Body = htmlMessage,
                    IsBodyHtml = true
                };

                message.To.Add(email);

                await client.SendMailAsync(message);
            }
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
        Task SendEmailAsync(string email, string subject, string callbackUrl);
        Task SendEmailAsync(string email, string callbackUrl);
        Task SendWelcomeEmailAsync(IdentityUser request);
    }
}
