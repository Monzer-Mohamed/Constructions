using Microsoft.EntityFrameworkCore;

namespace ApplicationForm.Data
{
    public class ApplicationFormDbContext : DbContext
    {

        public ApplicationFormDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entity.ApplicationForm> ApplicationForm { get; set; }
    }
}
