namespace DiscussionNet.Domain.Entities
{
    public class FeedItem
    {
        public string Subject { get; set; }
        public int CommentCount { get; set; }
        public int Url { get; set; }
        public DateTime LastCommentCreationDate { get; set; }
    }
}
