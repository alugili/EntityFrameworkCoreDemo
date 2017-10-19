using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.Relationships.ManyToMany
{
  public class ManyToManyRelationshipDbContext : DbContext
  {
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public ManyToManyRelationshipDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CustomerOrder>().HasKey(t => new { t.CustomerId, t.OrderId });

      modelBuilder.Entity<CustomerOrder>()
        .HasOne(pt => pt.Customer)
        .WithMany(p => p.Orders)
        .HasForeignKey(pt => pt.CustomerId);

      modelBuilder.Entity<CustomerOrder>()
        .HasOne(pt => pt.Order)
        .WithMany(t => t.Customers)
        .HasForeignKey(pt => pt.OrderId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ManyToManyRelationshipDatabase;Trusted_Connection=True;");
    }
  }

  public class Customer
  {
    public int CustomerId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<CustomerOrder> Orders { get; set; } = new Collection<CustomerOrder>();
  }

  public class CustomerOrder
  {
    public int OrderId { get; set; }

    public Order Order { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
  }

  public class Order
  {
    public int OrderId { get; set; }

    public int Number { get; set; }

    public string Description { get; set; }

    public ICollection<CustomerOrder> Customers { get; set; } = new Collection<CustomerOrder>();
  }
}