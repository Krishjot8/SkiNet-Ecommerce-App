using ECommerce_App.Models.Identity;

namespace ECommerce_App.Services
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
