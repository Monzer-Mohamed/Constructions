using Microsoft.EntityFrameworkCore;

namespace UserAccount.Data
{
    public class UserAccountDbContext : DbContext
    {
        public UserAccountDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Entities.UserAccount> UserAccount { get; set; }
    }
}
