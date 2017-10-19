using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Relationships.OneToMany
{
  public class OneToManyRelationshipDbContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public OneToManyRelationshipDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // modelBuilder.Entity<Order>().HasOne(s => s.Customer).WithMany(c => c.Orders);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=OneToManyRelationshipDatabase;Trusted_Connection=True;");
    }
  }

  public class Customer
  {
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; } = new Collection<Order>();
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