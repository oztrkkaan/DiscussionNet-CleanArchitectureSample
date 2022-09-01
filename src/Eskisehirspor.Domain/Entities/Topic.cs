using Eskisehirspor.Domain.Common;

namespace Eskisehirspor.Domain.Entities
{
    public class Topic : AuditableEntity
    {
        public const int SUBJECT_MIN_LENGTH = 1;
        public const int SUBJECT_MAX_LENGTH = 60;

        public Topic(string subject, List<Tag> tags)
        {
            SetTag(tags);
            SetSubject(subject);
        }
        public string Subject { get; private set; }
        public string UrlName => Subject;
        public List<Tag> Tags { get; private set; }

        public void SetTag(List<Tag> tags)
        {
            Tags = tags;
        }
        private void SetSubject(string subject)
        {
            bool isValidSubject = IsValidSubject(subject);
            if (!isValidSubject)
            {
                throw new Exception();
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
    }
}
