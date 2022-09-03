using Eskisehirspor.Domain.Common;

namespace Eskisehirspor.Domain.Entities
{
    public class UserEmailVerification : AuditableEntity
    {
        private const int EXPIRATION_DAYS = 7;
        public UserEmailVerification(User user)
        {
            User = user;
            SetGuid();
        }
        public UserEmailVerification() { }
        public Guid Guid { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsValid { get; private set; }
        public DateTime? ValidationDate { get; private set; }
        public User User { get; private set; }
        public bool IsExpired => ExpirationDate > DateTime.Now;
        private void SetGuid()
        {
            Guid = Guid.NewGuid();
        }
        public void SetExpirationDate()
        {
            ExpirationDate = DateTime.Now.AddDays(EXPIRATION_DAYS).ToUniversalTime();
        }
        public void SetAsVerified()
        {
            IsValid = true;
        }
    }
}
