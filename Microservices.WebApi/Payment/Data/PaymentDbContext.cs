using Microsoft.EntityFrameworkCore;

namespace Payment.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Entities.Payment> Payment { get; set; }
    }


}
