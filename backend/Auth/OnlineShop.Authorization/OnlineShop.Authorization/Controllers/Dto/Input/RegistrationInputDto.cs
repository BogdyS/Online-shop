using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Authorization.Controllers.Dto.Input
{
    public class RegistrationInputDto
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
