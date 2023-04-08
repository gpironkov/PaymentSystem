using Microsoft.EntityFrameworkCore;
using API.PaymentSystem.Data.Models;
using System;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(m => m.Merchant)
                .WithOne(u => u.User)
                //.HasForeignKey<Merchant>(u => u.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Merchant>()
                .HasMany(m => m.Transactions)
                .WithOne(t => t.Merchant)
                .HasForeignKey(t => t.MerchantId);
        }
    }
}
