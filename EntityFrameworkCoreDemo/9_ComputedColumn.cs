using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.ComputedColumn
{
  public class ComputedColumnDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public ComputedColumnDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer (@"Server=(localdb)\mssqllocaldb;Database=9_ComputedColumnDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Computed columns with Fluent API.
      modelBuilder.Entity<User>().Property(p => p.DisplayName).HasComputedColumnSql("[LastName] + ', ' + [FirstName]");
    }
  }

  public class User
  {
    public int Id { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public string DisplayName { get; set; }
  }
}