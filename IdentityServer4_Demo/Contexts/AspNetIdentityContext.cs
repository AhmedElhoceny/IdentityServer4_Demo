using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer4_Demo.Contexts
{
    public class AspNetIdentityContext : IdentityDbContext
    {
        public AspNetIdentityContext(DbContextOptions<AspNetIdentityContext> options):base(options){}
    }
}
