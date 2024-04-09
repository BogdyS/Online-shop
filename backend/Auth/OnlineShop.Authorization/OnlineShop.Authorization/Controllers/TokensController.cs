using Microsoft.AspNetCore.Mvc;
using OnlineShop.Authorization.Controllers.Dto.Input;
using Persistence;
using Services;

namespace OnlineShop.Authorization.Controllers
{
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly AuthorizationContext _context;
        private readonly IAuthService _authService;

        public TokensController(
            AuthorizationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        [HttpPost("api/authorize")]
        public async Task<IActionResult> AuthorizeByTokensAsync([FromBody] AuthorizeByTokensInput input)
        {
            var result = await _authService.AuthorizeByTokensAsync(input.AccessToken, input.RefreshToken);
            if (result is { Success:true , NewTokens: { } })
            {
                return Ok(result.NewTokens);
            }

            if (result is { Success: true, NewTokens: null })
            {
                return NoContent();
            }

            return Unauthorized();
        }
    }
}
