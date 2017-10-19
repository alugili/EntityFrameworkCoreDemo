using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreDemo.DatabaseGeneratedOption
{
  public class DatabaseGeneratedOption : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DatabaseGeneratedOption()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=8_DatabaseGeneratedOptionDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //// Value generated on add fluent API.
      //modelBuilder.Entity<User>().Property(b => b.Id).ValueGeneratedOnAdd();
    }
  }

  public class User
  {
    [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string DisplayName { get; set; }
  }
}