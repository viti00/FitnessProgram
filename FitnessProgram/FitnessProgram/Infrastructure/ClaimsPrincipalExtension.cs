
namespace FitnessProgram.Infrastructure
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            string id = null;

            if (user.Identity.IsAuthenticated == true)
            {
                id = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            return id;
            
        }
    }

}
