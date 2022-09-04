using Eskisehirspor.Application.Common.Interfaces;
using Eskisehirspor.Application.Common.Mailing;
using Microsoft.Extensions.Configuration;
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

        public List<MailMessage> Create(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null)
        {
            var emailCredential = GetMail(mailName);
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


        public EmailCredential GetMail(MailName emailName)
        {
            return _configuration.GetSection($"EmailCredentials:{emailName}").Get<EmailCredential>();
        }

    }
}
