namespace Services.Domain
{
    public class LogoutResult : BaseResult
    {
        public static LogoutResult SuccessResult => new LogoutResult { Success = true };
        public static LogoutResult FailureResult => new LogoutResult { Success = false };
    }
}
