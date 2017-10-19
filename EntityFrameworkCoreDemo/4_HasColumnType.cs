using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.HasColumnType
{
  public class HasColumnTypeDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public HasColumnTypeDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=4_HasColumnTypeDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //// Define data types with Fluent API.
      //modelBuilder.Entity<User>().Property(b => b.FirstName).HasColumnType("varchar(200)");
    }
  }

  public class User
  {
    public int Id { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string FirstName { get; set; }
  }
}