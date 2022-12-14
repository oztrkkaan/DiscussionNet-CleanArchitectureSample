using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Exceptions;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class Thread : AuditableEntity, ISoftDelete
    {
        private const int CONTENT_MIN_LENGTH = 1;
        private const string CONTENT_LENGTH_ERROR_MESSAGE = "İçerik daha uzun olmalı.";
        public Thread(string content, Topic topic, User user, string ipAddress)
        {
            SetContent(content);
            SetTopic(topic);
            SetUser(user);
            SetIpAddess(ipAddress);
        }
        public Thread() { }

        public string Content { get; private set; }
        public Topic Topic { get; private set; }
        public User User { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }
        public ThreadStatus Status { get; set; }
        public int? ParentThreadId { get; set; }
        public bool IsComment => ParentThreadId == null;
        public string IpAddress { get; private set; }
        public ICollection<ThreadReaction> Reactions { get; set; }

        public enum ThreadStatus
        {
            Hidden,
            Active,
            DeletedByAdministrator,
            Draft,
        }
        public void SetUser(User user)
        {
            User = user;
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
                throw new CustomException(CONTENT_LENGTH_ERROR_MESSAGE, true);
            }
            Content = content;
        }

        private bool IsVerifiedContent(string content)
        {
            if (content.Length <= CONTENT_MIN_LENGTH)
            {
                throw new Exception($"Content minimum length must be longer than {CONTENT_MIN_LENGTH} character.");
            }

            return true;
        }
        private void SetTopic(Topic topic)
        {
            Topic = topic;
        }
        private void SetIpAddess(string ipAddress)
        {
            IpAddress = ipAddress;
        }
    }
}
