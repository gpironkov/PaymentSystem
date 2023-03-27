using Microsoft.EntityFrameworkCore;
using API.PaymentSystem.Data.Models;

namespace API.PaymentSystem.Data
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Merchant> Merchants { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
