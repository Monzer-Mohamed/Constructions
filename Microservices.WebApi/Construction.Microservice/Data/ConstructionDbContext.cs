using Microsoft.EntityFrameworkCore;

namespace Construction.Data
{
    public class ConstructionDbContext : DbContext
    {

        public ConstructionDbContext(DbContextOptions<ConstructionDbContext> options) : base(options)
        {
        }

        public DbSet<Microservice.Entities.Construction> Construction { get; set; }
    }
}
