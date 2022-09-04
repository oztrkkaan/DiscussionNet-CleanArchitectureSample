
namespace Eskisehirspor.Infrastructure.Email
{
    public class EmailCredential
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string SmtpClient { get; set; }
    }
}
