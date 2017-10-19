using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Inheritance.TablePerHierarchy
{
  public class TablePerHierarchyDbContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<PowerCustomer> PowerCustomers { get; set; }

    public TablePerHierarchyDbContext()
    {
      this.Database.EnsureCreated();
      this.AddSmartInspectLogs();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // modelBuilder.Entity<PowerCustomer>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TablePerHierarchyDatabase;Trusted_Connection=True;");
    }
  }

  public class Customer
  {
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
  }

  public class PowerCustomer : Customer
  {
    public int Disccount { get; set; }
  }
}