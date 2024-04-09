using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entity;
using Services.Domain;

namespace Services
{
    public interface IAuthService
    {
        Task<AuthorizationResult> GetUserTokensAsync(User user);
        //Task<AuthorizationResult> SignInAsync(string login, string password);
        Task<AuthorizationResult> SignUpAsync(string login, string password);
        Task<TokenAuthorizationResult> AuthorizeByTokensAsync(Guid accessToken, Guid? refreshToken);
        Task<LogoutResult> LogoutAsync(Guid accessToken);
    }

    public class AuthService : IAuthService
    {
        private readonly AuthorizationContext _context;
        private readonly UserManager<User> _userManager;

        public AuthService(
            AuthorizationContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<TokenAuthorizationResult> AuthorizeByTokensAsync(Guid accessToken, Guid? refreshToken)
        {
            var utcNow = DateTime.UtcNow;

            var accessTokenInfo = await _context.TokenStorage
                .Where(x => x.Type == TokenType.Access)
                .Where(x => x.Value == accessToken)
                .SingleOrDefaultAsync();

            if (accessTokenInfo == null)
            {
                return TokenAuthorizationResult.Failure();
            }

            if (accessTokenInfo.ExpirationTime < utcNow)
            {
                if (refreshToken.HasValue)
                {
                    var refreshTokenInfo = await _context.TokenStorage
                        .AsTracking()
                        .Include(x => x.User)
                        .Where(x => x.Type == TokenType.Refresh)
                        .Where(x => x.Value == refreshToken)
                        .SingleOrDefaultAsync();

                    if (refreshTokenInfo == null || refreshTokenInfo.ExpirationTime < utcNow)
                    {
                        return TokenAuthorizationResult.Failure();
                    }

                    var newTokens = await GetUserTokensAsync(refreshTokenInfo.User);
                    _context.TokenStorage.Remove(refreshTokenInfo);
                    return TokenAuthorizationResult.SuccessResult(newTokens);
                }
            }
            return TokenAuthorizationResult.SuccessResult();
        }

        public async Task<AuthorizationResult> GetUserTokensAsync(User user)
        {
            var response = new AuthorizationResult
            {
                AccessToken = Guid.NewGuid(),
                RefreshToken = Guid.NewGuid(),
            };

            var utcNow = DateTime.UtcNow;

            _context.TokenStorage.Add(new Token
            {
                ExpirationTime = utcNow.AddMinutes(10), // TODO: Add options
                Type = TokenType.Access,
                User = user,
                Value = response.AccessToken,
            });

            _context.TokenStorage.Add(new Token
            {
                ExpirationTime = utcNow.AddHours(5), // TODO: Add options
                Type = TokenType.Refresh,
                User = user,
                Value = response.RefreshToken,
            });

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<LogoutResult> LogoutAsync(Guid accessToken)
        {
            var utcNow = DateTime.UtcNow;

            var accessTokenInfo = await _context.TokenStorage
                .Where(x => x.Type == TokenType.Access)
                .Where(x => x.Value == accessToken)
                .SingleOrDefaultAsync();

            if (accessTokenInfo == null)
            {
                return LogoutResult.FailureResult;
            }

            var userId = accessTokenInfo.UserId;

            await _context.TokenStorage
                .Where(x => x.UserId == userId)
                .Where(x => x.ExpirationTime > utcNow)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.ExpirationTime, utcNow));

            return LogoutResult.SuccessResult;
        }

        //public Task<AuthorizationResult> SignInAsync(string login, string password)
        //{
        //    var result = _userManager.check
        //}

        public async Task<AuthorizationResult> SignUpAsync(string login, string password)
        {
            using var transactionContext = await _context.Database.BeginTransactionAsync();
            var user = new User
            {
                Email = login,
                UserName = login,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var response = await GetUserTokensAsync(user);

                await transactionContext.CommitAsync();
                return response;
            }

            await transactionContext.RollbackAsync();
            throw new ApplicationException(result.Errors.First().Description);
        }
    }
}