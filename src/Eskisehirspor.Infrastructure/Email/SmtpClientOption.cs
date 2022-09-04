using System.Net;
using System.Net.Mail;

namespace Eskisehirspor.Infrastructure.Email
{
    public static class SmtpClientOption
    {
        public static void SendMail(IList<MailMessage> mailMessages, EmailCredential emailCredential)
        {
            foreach (var mailMessage in mailMessages)
            {
                SmtpClient smtpClient = new(emailCredential.SmtpClient);
                smtpClient.Port = emailCredential.Port;
                smtpClient.Credentials = new NetworkCredential(emailCredential.Email, emailCredential.Password);
                smtpClient.EnableSsl = emailCredential.EnableSsl;

                smtpClient.Send(mailMessage);
            }
        }
    }
}
