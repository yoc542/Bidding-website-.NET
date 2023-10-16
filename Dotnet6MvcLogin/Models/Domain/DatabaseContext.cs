using Dotnet6MvcLogin.Models.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet6MvcLogin.Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }


        public DbSet<ApplicationForm> ApplicationForms { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}
