using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.DefaultValues
{
  public class DefaultValuesDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DefaultValuesDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=5_DefaultValuesDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //// Define default values with Fluent API.
      //modelBuilder.Entity<User>().Property(b => b.FirstName).HasDefaultValue("Bassam");
    }
  }

  public class User
  {
    public int Id { get; set; }

    // Define default with code.
    public string FirstName { get; set; } = "Bassam";
  }
}