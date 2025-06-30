using OnlineCoursesPlatform.Application.Abstractions.Services;
using OnlineCoursesPlatform.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace OnlineCoursesPlatform.Application.Services.Email
{
    public class GmailEmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<GmailEmailService> _logger;

        public GmailEmailService(IOptions<EmailSettings> emailSettings, ILogger<GmailEmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_emailSettings.SmtpServer)
                {
                    Port = _emailSettings.Port,
                    Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.AppPassword),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);

                _logger.LogInformation("Email successfully sent to {Recipient}", to);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email to {Recipient}", to);
                throw;
            }
        }
    }
}
