using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.PrimaryKey
{
  public class PrimaryKeyDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public PrimaryKeyDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=2_PrimaryKeyDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //  // Define the primary key Fluent API. 
      //  modelBuilder.Entity<User>().HasKey(c => c.Id);

      //  // Define the composite primary key Fluent API. 
      // modelBuilder.Entity<UserPrimaryKeyDemo>().HasKey(c => new { c.Id, c.EMail });
    }
  }

  public class User
  {
    // Define the primary key data Annotations. 
    [Key]
    // [Column(Order = 0), Key]
    public int Id { get; set; }

    // Define the composite primary key Fluent API. 
    // [Column(Order = 1), Key]
    // Set required and maximum Length with Fluent API.
    // [Required, MaxLength(500)]
    public string EMail { get; set; }
  }
}