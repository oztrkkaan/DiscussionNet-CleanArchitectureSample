using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Application.Common.Mailing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Eskisehirspor.Infrastructure.Email
{
    public class MailService : IMailService
    {
        IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<MailMessage> CreateMailMessage(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null)
        {
            var emailCredential = GetEmailCredential(mailName);
            List<MailMessage> mailMessages = new();
            MailMessage mailMessage = new();

            foreach (var email in emailsToSend)
            {
                mailMessage = new();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(emailCredential.Email);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                if (attachment is not null)
                {
                    mailMessage.Attachments.Add(attachment);
                }
                mailMessages.Add(mailMessage);
            }
            return mailMessages;
        }

        public async Task SendMailAsync(IList<MailMessage> mailMessages, EmailCredential emailCredential)
        {
            foreach (var mailMessage in mailMessages)
            {
                using SmtpClient smtpClient = new(emailCredential.SmtpClient);
                smtpClient.Port = emailCredential.Port;
                smtpClient.Credentials = new NetworkCredential(emailCredential.Email, emailCredential.Password);
                smtpClient.EnableSsl = emailCredential.EnableSsl;
                await smtpClient.SendMailAsync(mailMessage);
            }
        }

        public async Task SendMailAsync(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null)
        {
            var mailCredentials = GetEmailCredential(mailName);
            var mailMessages = CreateMailMessage(subject, body, emailsToSend, mailName, attachment);
            await SendMailAsync(mailMessages, mailCredentials);
        }

        private EmailCredential GetEmailCredential(MailName emailName)
        {
            return _configuration.GetSection($"EmailCredentials:{emailName}").Get<EmailCredential>();
        }

    }
}
