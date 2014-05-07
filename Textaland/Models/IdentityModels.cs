using Microsoft.AspNet.Identity.EntityFramework;

namespace Textaland.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class UserAccount : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<UserAccount>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}