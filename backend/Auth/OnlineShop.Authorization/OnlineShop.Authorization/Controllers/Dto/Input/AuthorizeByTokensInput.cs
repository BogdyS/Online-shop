using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Authorization.Controllers.Dto.Input
{
    public class AuthorizeByTokensInput
    {
        [Required]
        public Guid AccessToken { get; set; }
        public Guid? RefreshToken { get; set; }
    }
}
