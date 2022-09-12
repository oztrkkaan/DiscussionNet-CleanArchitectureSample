namespace DiscussionNet.Domain.Common
{
    public abstract class AuditableEntity : Entity<int>
    {
        public AuditableEntity()
        {
            SetCreationDate();
        }
        public DateTime CreationDate { get; private set; }
        public DateTime? ModifiedDate { get; private set; }

        public void SetCreationDate()
        {
            CreationDate = DateTime.Now;
        }
        public void SetModifiedDate()
        {
            ModifiedDate = DateTime.Now;
        }
    }
}
