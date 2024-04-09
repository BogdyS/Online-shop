using Microsoft.AspNetCore.Identity;

namespace Persistence.Entity
{
    public class User : IdentityUser
    {
        public List<Token> Tokens { get; set; }
    }
}
