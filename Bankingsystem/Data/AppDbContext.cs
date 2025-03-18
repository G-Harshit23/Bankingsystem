using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bankingsystem.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bankingsystem.Data
{
    public class AppDbContext : DbContext
    {
       
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("Server=HARSHITGUNDA;Database=BankingSystem;Trusted_Connection=True;TrustServerCertificate=True");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in Database Connection:{e.Message} ");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Balance)
                    .HasDefaultValue(0.00);
            });

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Accounts)
                .WithMany() 
                .HasForeignKey(t => t.AccountId) 
                .OnDelete(DeleteBehavior.Cascade); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
