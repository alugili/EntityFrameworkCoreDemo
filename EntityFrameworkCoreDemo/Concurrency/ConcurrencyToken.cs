using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreDemo.Concurrency.ConcurrencyToken
{
  public class ConcurrencyTokenDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public ConcurrencyTokenDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ConcurrencyTokenDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<User>()
      //  .Property(p => p.LastName)
      //  .IsConcurrencyToken();
    }
  }

  public class User
  {
    public int Id { get; set; }

    [ConcurrencyCheck]
    public string FirstName { get; set; }

    public string LastName { get; set; }
  }
}