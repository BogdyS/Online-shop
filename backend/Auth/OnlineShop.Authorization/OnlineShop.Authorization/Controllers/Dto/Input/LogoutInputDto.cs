using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Authorization.Controllers.Dto.Input
{
    public class LogoutInputDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
