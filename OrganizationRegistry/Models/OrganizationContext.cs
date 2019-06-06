using Microsoft.EntityFrameworkCore;

namespace OrganizationApi.Models
{
    public class OrganizationContext : DbContext
    {
        public OrganizationContext(DbContextOptions<OrganizationContext> options)
            : base(options)
        {
        }

        public DbSet<OrganizationItem> OrganizationItems{ get; set; }


    }
}