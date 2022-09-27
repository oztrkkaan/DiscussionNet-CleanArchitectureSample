namespace DiscussionNet.Domain.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(string message, bool isLogging = false) : base(message)
        {
            IsLogging = isLogging;
            SetErrorCode();
        }
        public CustomException(string message, Exception innerException, bool isLogging = false) : base(message, innerException)
        {
            IsLogging = isLogging;
            SetErrorCode();
        }
        public bool IsLogging { get; }
        public string ErrorCode { get; private set; }

        private static string GenerateErrorCode()
        {
            return Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        }
        private void SetErrorCode()
        {
            if (!IsLogging) return;
            ErrorCode = GenerateErrorCode();
        }
    }
}
