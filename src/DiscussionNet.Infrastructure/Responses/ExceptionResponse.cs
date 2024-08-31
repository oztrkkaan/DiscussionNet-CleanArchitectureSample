namespace DiscussionNet.Infrastructure.Responses
{
    public class ExceptionResponse
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
    }
}
