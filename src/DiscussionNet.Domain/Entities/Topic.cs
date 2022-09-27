using DiscussionNet.Domain.Common;
using DiscussionNet.Domain.Exceptions;
using DiscussionNet.Domain.Interfaces;

namespace DiscussionNet.Domain.Entities
{
    public class Topic : AuditableEntity, ISoftDelete
    {
        public const int SUBJECT_MIN_LENGTH = 1;
        public const int SUBJECT_MAX_LENGTH = 60;
        public const string SUBJECT_LENGTH_ERROR_MESSAGE = "Başlık uzunluğu {0} ve {1} karakter aralığında olmalıdır.";

        public Topic(string subject, List<Tag> tags = null)
        {
            SetTag(tags);
            SetSubject(subject);
        }
        public Topic() { }
        public string Subject { get; private set; }
        public string UrlName => Subject;
        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public ICollection<Tag> Tags { get; private set; }
        public ICollection<Thread> Threads { get; private set; }
        public int ThreadCount => Threads.Count();

        public void SetTag(List<Tag> tags)
        {
            Tags = tags;
        }
        private void SetSubject(string subject)
        {
            bool isValidSubject = IsValidSubject(subject);
            if (!isValidSubject)
            {
                throw new CustomException(string.Format(SUBJECT_LENGTH_ERROR_MESSAGE, SUBJECT_MIN_LENGTH, SUBJECT_MAX_LENGTH), true);
            }
            Subject = subject;
        }
        private bool IsValidSubject(string subject)
        {
            if (subject.Length < SUBJECT_MIN_LENGTH || subject.Length > SUBJECT_MAX_LENGTH)
            {
                return false;
            }
            return true;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            DeletionDate = DateTime.Now;
        }
    }
}
