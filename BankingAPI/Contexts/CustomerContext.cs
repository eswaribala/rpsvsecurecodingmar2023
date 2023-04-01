
using BankingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingAPI.Contexts
{
    public class CustomerContext:DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) :
            base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Corporate> Corporates { get; set; }
        public DbSet<Address> Addresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Individual>().ToTable("Individual");
            modelBuilder.Entity<Corporate>().ToTable("Corporate");
            modelBuilder.Entity<Address>().ToTable("Address");
        }
    }
}
