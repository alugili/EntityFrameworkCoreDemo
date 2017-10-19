using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.RequiredAndMaxLength
{
  public class RequiredAndMaxLengthDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public RequiredAndMaxLengthDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=7_RequiredAndMaxLengthDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //// Set required and maximum Length with Fluent API.
      //modelBuilder.Entity<User>().Property(b => b.FirstName).IsRequired().HasMaxLength(500);
    }
  }

  public class User
  {
    public int Id { get; set; }

    [Required, MaxLength(500)]
    public string FirstName { get; set; }
  }
}