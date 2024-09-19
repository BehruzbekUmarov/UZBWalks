using Microsoft.AspNetCore.Identity;

namespace UZBWalks.Api.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
