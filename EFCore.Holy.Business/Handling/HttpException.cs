namespace EFCore.Holy.Business.Handling
{
    public class HttpException : Exception
    {
        public int StatusCode { get; }
        public string Message { get; }

        public HttpException(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}
