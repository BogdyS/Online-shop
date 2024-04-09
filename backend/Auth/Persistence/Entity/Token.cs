using Microsoft.AspNetCore.Identity;

namespace Persistence.Entity
{
    public class Token
    {
        public int Id { get; set; }
        public TokenType Type { get; set; }
        public DateTime ExpirationTime { get; set; }
        public Guid Value { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
    }

    public enum TokenType
    {
        Access,
        Refresh
    }
}
