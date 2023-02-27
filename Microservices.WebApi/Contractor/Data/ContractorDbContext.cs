using Microsoft.EntityFrameworkCore;

namespace Contractor.Data
{
    public class ContractorDbContext : DbContext
    {
        public ContractorDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Contractor> Contractor { get; set; }


    }
}
