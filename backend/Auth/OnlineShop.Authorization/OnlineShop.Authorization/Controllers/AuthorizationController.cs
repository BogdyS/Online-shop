using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Authorization.Controllers.Dto;
using OnlineShop.Authorization.Controllers.Dto.Input;
using Persistence;
using Persistence.Entity;
using Services;

namespace OnlineShop.Authorization.Controllers
{
    [Authorize]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthorizationController(
            UserManager<User> userManager,
            AuthorizationContext context,
            IAuthService authService)
        {
            _userManager = userManager;
            _context = context;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("api/sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] RegistrationInputDto input)
        {
            var result = await _authService.SignUpAsync(input.Login, input.Password);
            return Ok(AuthorizationResultDto.FromAuthorizationResult(result));
        }

        [AllowAnonymous]
        [HttpPost("api/sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInByPasswordInputDto input)
        {
            var user = await _userManager.FindByNameAsync(input.Login);

            if (user == null)
            {
                return Unauthorized();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            var response = await _authService.GetUserTokensAsync(user);

            return Ok(AuthorizationResultDto.FromAuthorizationResult(response));
        }

        //[HttpPost("api/logout")]
        //public async Task<IActionResult> LogoutAsync([FromBody] LogoutInputDto input)
        //{
        //    var user 
        //    return Ok();
        //}
    }
}
