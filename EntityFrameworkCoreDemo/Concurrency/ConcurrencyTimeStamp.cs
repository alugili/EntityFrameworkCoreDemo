using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreDemo.Concurrency.TimeStamp
{
  public class ConcurrencyTimeStampDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public ConcurrencyTimeStampDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ConcurrencyTimeStampDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<User>()
      //.Property(p => p.Timestamp)
      //.ValueGeneratedOnAddOrUpdate()
      //.IsConcurrencyToken();
    }
  }

  public class User
  {
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Timestamp]
    public byte[] Timestamp { get; set; }
  }
}