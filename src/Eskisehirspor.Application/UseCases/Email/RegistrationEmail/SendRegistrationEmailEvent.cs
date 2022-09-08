using Eskisehirspor.Application.Common.Interfaces;
using MediatR;

namespace Eskisehirspor.Application.UseCases.Email.RegistrationEmail
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
        private const string EMAIL_SUBJECT = "E-Posta Doğrulama - eskisehirspor.com";

        public SendRegistrationEmailEventHandler(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task Handle(SendRegistrationEmailEvent notification, CancellationToken cancellationToken)
        {
            var emailBody =
            $@"Aramıza hoşgeldin {notification.DisplayName}, 
            <br/><br/>
            <a href='https://eskisehirspor.com'>eskisehirspor.com</a> hesabınıza {notification.Email} e-posta adresinizi kullanarak veya <b>{notification.Username}</b> kullanıcı adıyla giriş yapabilirsiniz.
            <br/><br/>
            E-posta adresinizi doğrulamak için aşağıdaki bağlantıyı tıkla:
            <a href='{notification.ActivationUrl}'>{notification.ActivationUrl}</a>
            ";

            await _mailService.SendMailAsync(EMAIL_SUBJECT, emailBody, new List<string> { notification.Email }, Common.Mailing.MailName.Bilgi);
        }
    }
}
