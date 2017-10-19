using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Relationships.OneToOne
{
  public class OneToOneRelationshipDbContext : DbContext
  {
    public DbSet<Order> Orders { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public OneToOneRelationshipDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.Entity<Customer>()
      //  .HasOne(p => p.Order)
      //  .WithOne(i => i.Customer)
      //  .HasForeignKey<Order>(b => b.CustomerId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OneToOneRelationshipDatabase;Trusted_Connection=True;");
    }
  }

  public class Customer
  {
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Order Order { get; set; }
  }

  public class Order
  {
    public int OrderId { get; set; }

    public int Number { get; set; }

    public string Description { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
  }
}