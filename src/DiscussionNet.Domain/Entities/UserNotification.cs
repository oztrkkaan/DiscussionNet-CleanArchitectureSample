using DiscussionNet.Domain.Common;

namespace DiscussionNet.Domain.Entities
{
    public class UserNotification : Entity<int>
    {
        public UserNotification(User user, Notification notification)
        {
            ReceiverUser = user;
            Notification = notification;
        }

        public UserNotification(){ }
        public DateTime? ReadDate { get; private set; }
        public bool IsRead { get; private set; }
        public User ReceiverUser { get; set; }
        public Notification Notification { get; set; }
        public void SetAsRead()
        {
            IsRead = true;
            ReadDate = DateTime.Now;
        }
    }
}
