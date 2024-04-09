using Services.Domain;

namespace OnlineShop.Authorization.Controllers.Dto
{
    public class AuthorizationResultDto
    {
        public Guid AccessToken { get; set; }
        public Guid RefreshToken { get; set; }

        public static AuthorizationResultDto FromAuthorizationResult(AuthorizationResult authorizationResultDto)
        {
            return new AuthorizationResultDto
            {
                AccessToken = authorizationResultDto.AccessToken,
                RefreshToken = authorizationResultDto.RefreshToken,
            };
        }
    }
}
