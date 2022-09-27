using DiscussionNet.Application.Common.Interfaces;
using DiscussionNet.Application.Common.Mailing;
using MediatR;

namespace DiscussionNet.Application.UseCases.Email.RegistrationEmail
{
    public class SendRegistrationEmailEvent : INotification
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string ActivationUrl { get; set; }
    }

    public class SendRegistrationEmailEventHandler : INotificationHandler<SendRegistrationEmailEvent>
    {
        private readonly IMailService _mailService;
        private const string EMAIL_SUBJECT = "E-Posta Doğrulama - discussionnet.com";

        public SendRegistrationEmailEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Handle(SendRegistrationEmailEvent notification, CancellationToken cancellationToken)
        {
            var emailBody =
            $@"Aramıza hoşgeldin {notification.DisplayName}, 
            <br/><br/>
            <a href='https://discussionnet.com'>discussionnet.com</a> hesabınıza {notification.Email} e-posta adresinizi kullanarak veya <b>{notification.Username}</b> kullanıcı adıyla giriş yapabilirsiniz.
            <br/><br/>
            E-posta adresinizi doğrulamak için aşağıdaki bağlantıyı tıkla:
            <a href='{notification.ActivationUrl}'>{notification.ActivationUrl}</a>
            ";

            await _mailService.SendMailAsync(EMAIL_SUBJECT, emailBody, new List<string> { notification.Email }, MailName.Bilgi);
        }
    }
}
