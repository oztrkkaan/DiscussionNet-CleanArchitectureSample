using Eskisehirspor.Application.Common.Mailing;
using System.Net.Mail;

namespace Eskisehirspor.Application.Common.Interfaces
{
    public interface IMailService
    {
        List<MailMessage> Create(string subject, string body, List<string> emailsToSend, MailName mailName, Attachment attachment = null);

    }
}
