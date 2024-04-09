namespace Services.Domain
{
    public class AuthorizationResult
    {
        public Guid AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
