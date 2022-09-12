namespace DiscussionNet.Application.Common.Security
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
