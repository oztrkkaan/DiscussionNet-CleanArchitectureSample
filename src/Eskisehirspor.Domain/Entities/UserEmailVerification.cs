using Eskisehirspor.Domain.Common;

namespace Eskisehirspor.Domain.Entities
{
    public class UserEmailVerification : AuditableEntity
    {
        private const int ExpirationDays = 7;
        public UserEmailVerification(User user)
        {
            User = user;
            SetGuid();
            SetCreationDate();
        }
        public Guid Guid { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsValid { get; private set; }
        public DateTime? ValidationDate { get; private set; }
        public User User { get; private set; }

        private void SetGuid()
        {
            Guid = new Guid();
        }
        public void SetExpirationDate()
        {
            ExpirationDate = DateTime.Now.AddDays(ExpirationDays);
        }
    }
}
