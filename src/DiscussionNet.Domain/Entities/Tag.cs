using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class Tag : AuditableEntity, ISoftDelete
    {
        public string Name { get; private set; }

        public bool IsDeleted { get; private set; }

        public DateTime? DeletionDate { get; private set; }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
    }
}
