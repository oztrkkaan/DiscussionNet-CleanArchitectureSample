using Eskisehirspor.Domain.Common;
using Eskisehirspor.Domain.Interfaces;

namespace Eskisehirspor.Domain.Entities
{
    public class Thread : AuditableEntity, ISoftDelete
    {
        public Thread(string content, Topic topic)
        {
            SetContent(content);
            SetTopic(topic);
            SetCreationDate();
        }

        public string Content { get; private set; }
        public Topic Topic { get; private set; }
        public User User { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }
        public int LikeCount { get; private set; }
        public int UnlikeCount { get; private set; }
        public ThreadStatus Status { get; set; }
        public int? ParentThreadId { get; set; }
        public bool IsComment => ParentThreadId == null;



        public enum ThreadStatus
        {
            Hidden,
            Active,
            DeletedByAdministrator,
            Draft,
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
        public void UpdateContent(string newContent)
        {
            SetContent(newContent);
            SetModifiedDate();
        }
        private void SetContent(string content)
        {
            var isVerifiedContent = IsVerifiedContent(content);
            if (!isVerifiedContent)
            {
                throw new Exception();
            }
            Content = content;
        }

        private bool IsVerifiedContent(string content)
        {
            //TODO:  
            return true;
        }
        private void SetTopic(Topic topic)
        {
            Topic = topic;
        }
    }
}
