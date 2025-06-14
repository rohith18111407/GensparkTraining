using Microsoft.AspNetCore.Identity;

namespace NotifyAPI.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}