using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class CommentReaction : AuditableEntity, ISoftDelete
    {
        public CommentReaction(Comment comment, Reactions reaction, User reactedBy)
        {
            SetComment(comment);
            SetReaction(reaction, false);
            SetUser(reactedBy);
        }
        public CommentReaction() { }
        public Comment Comment { get; private set; }
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
        private void SetComment(Comment comment)
        {
            Comment = comment;
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
