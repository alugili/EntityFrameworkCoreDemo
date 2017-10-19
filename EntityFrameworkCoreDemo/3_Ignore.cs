using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Ignore
{
  public class IgnoreDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public IgnoreDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=3_IgnoreDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //// Excluding data type with Fluent API.
      //modelBuilder.Ignore<IgnoredUser>();

      //// Excluding properties with Fluent API.
      //modelBuilder.Entity<User>().Ignore(b => b.LastName);
    }
  }

  public class User
  {
    public int Id { get; set; }

    public string FirstName { get; set; }

    // Excluding properties with data Annotations.
    [NotMapped]
    public string LastName { get; set; }
  }

  // Excluding data type with data Annotations.
  [NotMapped]
  public class IgnoredUser
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public ICollection<int> Permissions { get; set; }
  }
}