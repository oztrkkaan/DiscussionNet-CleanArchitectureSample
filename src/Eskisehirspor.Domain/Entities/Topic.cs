using Eskisehirspor.Domain.Common;

namespace Eskisehirspor.Domain.Entities
{
    public class Topic : AuditableEntity
    {
        public Topic(string subject, List<Tag> tags)
        {
            ThrowExceptionIfTagsNullOrEmpty(tags);
            SetSubject(subject);
        }
        public string Subject { get; private set; }
        public string UrlName => Subject;
        public List<Tag> Tags { get; private set; }

        public void SetTag(List<Tag> tags)
        {
            ThrowExceptionIfTagsNullOrEmpty(tags);
        }
        private static void ThrowExceptionIfTagsNullOrEmpty(List<Tag> tags)
        {
            if (tags == null || tags.Any())
            {
                throw new Exception();
            }
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
            //TODO: subject kuralları yazılmalı
            return true;
        }
    }
}
