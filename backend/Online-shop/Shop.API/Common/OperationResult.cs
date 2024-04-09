namespace Shop.API.Common
{
    public class OperationResult
    {
        public OperationResult()
        {
        }
        public OperationResult(object obj)
        {
            Result = obj;
        }
        public object Result { get; set; }

        public Exception Error { get; set; }
    }
}
