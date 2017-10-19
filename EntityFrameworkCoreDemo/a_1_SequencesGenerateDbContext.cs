using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.SequencesGenerate
{
  public class SequencesGenerateDbContext : DbContext
  {
    public DbSet<Order> Orders { get; set; }

    public SequencesGenerateDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer (@"Server=(localdb)\mssqllocaldb;Database=a_1_SequencesGenerateDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasSequence<int>("OrderNumbers", schema: "shared")
        .StartsAt(1000)
        .IncrementsBy(5);

      modelBuilder.Entity<Order>()
        .Property(o => o.OrderNo)
        .HasDefaultValueSql("NEXT VALUE FOR shared.OrderNumbers");
    }
  }

  public class Order
  {
    public int OrderId { get; set; }

    public int OrderNo { get; set; }

    public string Description { get; set; }
  }
}