namespace Shop.API.Common
{
    public class HttpExceptionResponse
    {
        public string Message { get; set; }

        public string StackTrace { get; set; }

        public int StatusCode { get; set; }
    }
}
