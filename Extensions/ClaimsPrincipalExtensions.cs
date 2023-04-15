using System.Security.Claims;

namespace ECommerce_App.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user) 
        
        { 
        
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
