namespace Services.Domain
{
    public class TokenAuthorizationResult : BaseResult
    {
        public AuthorizationResult? NewTokens { get; set; } = null;

        public static TokenAuthorizationResult Failure()
            => new TokenAuthorizationResult { Success = false };

        public static TokenAuthorizationResult SuccessResult(AuthorizationResult? tokens = null)
            => new TokenAuthorizationResult
            {
                Success = true,
                NewTokens = tokens
            };
    }
}
