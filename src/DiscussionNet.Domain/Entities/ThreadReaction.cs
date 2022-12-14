using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class ThreadReaction : AuditableEntity, ISoftDelete
    {
        public ThreadReaction(Thread thread, Reactions reaction, User reactedBy)
        {
            SetThread(thread);
            SetReaction(reaction, false);
            SetUser(reactedBy);
        }
        public ThreadReaction() { }
        public Thread Thread { get; private set; }
        public User ReactedBy { get; private set; }
        public Reactions Reaction { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }
        public void SetReaction(Reactions reaction, bool isModified)
        {
            Reaction = reaction;
            if (isModified)
            {
                SetModifiedDate();
            }
        }
        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
        private void SetThread(Thread thread)
        {
            Thread = thread;
        }
        private void SetUser(User user)
        {
            ReactedBy = user;
        }
        public enum Reactions
        {
            Like = 1,
            Unlike = 2
        }
    }
}
