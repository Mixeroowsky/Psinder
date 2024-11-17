using Microsoft.AspNetCore.Identity;

namespace Psinder.Server.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
