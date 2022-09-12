using DiscussionNet.Application.Common.Mailing;
using System.Net.Mail;

namespace DiscussionNet.Application.Common.Interfaces
{
    public interface IMailService
    {
        List<MailMessage> CreateMailMessage(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null);
        Task SendMailAsync(IList<MailMessage> mailMessages, EmailCredential emailCredential);
        Task SendMailAsync(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null);

    }
}
