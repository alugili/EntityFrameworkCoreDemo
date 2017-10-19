using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Uniqueness
{
  public class UniquenessDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public UniquenessDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=6_UniquenessDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Indexes with fluent API.
      modelBuilder.Entity<User>().HasAlternateKey(c => c.FirstName).HasName("IX_FirstName_Unique");
      modelBuilder.Entity<User>().HasIndex(b => b.LastName).IsUnique();
    }
  }

  public class User
  {
    public int Id { get; set; }

    // Indexes with data Annotations, see the StackOverflow Link.
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}