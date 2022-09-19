using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class Notification : AuditableEntity, ISoftDelete
    {
        public const int CONTENT_MAX_LENGTH = 256;
        public const int URL_MAX_LENGTH = 256;

        public Notification(string content, string url)
        {
            SetContent(content);
            SetUrl(url);
        }
        internal Notification(int notificationId)
        {
            Id = notificationId;
        }

        public string Content { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }
        public string Url { get; private set; }
        public bool IsPassive { get; private set; }
        public ICollection<User> ReceiverUsers { get; set; }
        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }

        public void SetContent(string content)
        {
            Content = content;
        }
        public void SetUrl(string url)
        {
            Url = url;
        }
        public void SetPassive()
        {
            IsPassive = true;
        }

    }
}
