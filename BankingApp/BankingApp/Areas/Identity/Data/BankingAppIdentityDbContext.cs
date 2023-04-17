using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Areas.Identity.Data;

public class BankingAppIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public BankingAppIdentityDbContext(DbContextOptions<BankingAppIdentityDbContext> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<CustomerIdentity> CustomerIdentity { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
