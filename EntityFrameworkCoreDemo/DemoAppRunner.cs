using System;
using System.Collections.ObjectModel;
using System.Linq;
using EntityFrameworkCoreDemo.Concurrency.ConcurrencyToken;
using EntityFrameworkCoreDemo.Concurrency.TimeStamp;
using EntityFrameworkCoreDemo.DefaultValues;
using EntityFrameworkCoreDemo.HasColumnType;
using EntityFrameworkCoreDemo.Ignore;
using EntityFrameworkCoreDemo.Inheritance.TablePerHierarchy;
using EntityFrameworkCoreDemo.PrimaryKey;
using EntityFrameworkCoreDemo.Relationships.ManyToMany;
using EntityFrameworkCoreDemo.Relationships.OneToMany;
using EntityFrameworkCoreDemo.Relationships.OneToOne;
using EntityFrameworkCoreDemo.RequiredAndMaxLength;
using EntityFrameworkCoreDemo.SimpleExample;
using EntityFrameworkCoreDemo.Uniqueness;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo
{
  public class DemoAppRunner
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("******************************* 1_SimpleExample Demo ******************************");
      using (var dbContext = new SimpleExampleDbContext())
      {
        //1-) Add Add-Migration in Package Manager Console 
        //2-) in the command do not forget to add SimpleExampleDbContext
        //3-) Remove  this.Database.EnsureCreated(); from SimpleExampleDbContext and enable the line below.
        //4-) dbContext.Database.Migrate();

        dbContext.Add(new Blog { Url = "Bassam Test" });
        dbContext.SaveChanges();
      }

      // Read the data
      using (var dbContext = new SimpleExampleDbContext())
      {

        dbContext.AddSmartInspectLogs();
        var blogs = dbContext.Blogs.ToList();

        // Raw SQL
        // users = dbContext.Blogs.FromSql("SELECT * FROM dbo.Blogs").ToList();
        Console.WriteLine($"Dump blogs: {blogs}");
      }

      Console.WriteLine("******************************* 2_PrimaryKey Demo ******************************");
      using (var dbContext = new PrimaryKeyDbContext())
      {
        dbContext.Users.Add(new PrimaryKey.User { EMail = "alugili@gmail.com" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 3_Ignore Demo ******************************");
      using (var dbContext = new IgnoreDbContext())
      {
        dbContext.Users.Add(new Ignore.User { FirstName = "Rami" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 4_HasColumnType Demo ******************************");
      using (var dbContext = new HasColumnTypeDbContext())
      {
        dbContext.Users.Add(new HasColumnType.User { FirstName = "Fadi" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 5_DefaultValues Demo ******************************");
      using (var dbContext = new DefaultValuesDbContext())
      {
        dbContext.Users.Add(new DefaultValues.User());
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 6_Uniqueness Demo ******************************");
      using (var dbContext = new UniquenessDbContext())
      {
        // dbContext.Users.Add(new Uniqueness.User { FirstName = "Mays", LastName = "Alhassun" });
        dbContext.SaveChanges();
      }

      Console.WriteLine(
        "******************************* 7_RequiredAndMaxLength Demo ******************************");
      using (var dbContext = new RequiredAndMaxLengthDbContext())
      {
        dbContext.Users.Add(new RequiredAndMaxLength.User { FirstName = "Bassam" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 8_DatabaseGeneratedOption Demo ******************************");
      using (var dbContext = new DatabaseGeneratedOption.DatabaseGeneratedOption())
      {
        // dbContext.Users.Add(new DatabaseGeneratedOption.User { Id = 42, FirstName = "Mays" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* 9_ComputedColumn Demo ******************************");
      using (var dbContext = new ComputedColumn.ComputedColumnDbContext())
      {
        dbContext.Users.Add(new ComputedColumn.User { FirstName = "Mays", LastName = "Alhassun" });
        dbContext.SaveChanges();

        var user = dbContext.Users.First();
        Console.WriteLine($"Dump User: {user}");

        user.FirstName = "Maysa";
        user = dbContext.Users.First();
        Console.WriteLine($"Dump User Computed name changed: {user}");
      }

      Console.WriteLine("******************************* a_1_SequencesGenerateDbContext Demo ******************************");
      using (var dbContext = new SequencesGenerate.SequencesGenerateDbContext())
      {
        dbContext.Orders.Add(new SequencesGenerate.Order { Description = "New Product!" });
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* OneToOneRelationship Demo ******************************");
      using (var dbContext = new OneToOneRelationshipDbContext())
      {
        var customer = new Relationships.OneToOne.Customer
        {
          FirstName = "Bassam",
          LastName = "Alugili",
          Order = new Relationships.OneToOne.Order
          {
            Description = "Good Product",
            Number = 42
          }
        };

        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* OneToManyRelationship Demo ******************************");
      using (var dbContext = new OneToManyRelationshipDbContext())
      {
        var customer = new Relationships.OneToMany.Customer
        {
          FirstName = "Bassam",
          LastName = "Alugili",
          Orders = new Collection<Relationships.OneToMany.Order>
              {
                new Relationships.OneToMany.Order
                {
                  Description = "Good Product",
                  Number = 42
                },
                new Relationships.OneToMany.Order
                {
                  Description = "Good Product",
                  Number = 42
                }
              }
        };

        dbContext.Customers.Add(customer);
        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* Eager/Explicit loading Demo ******************************");
      using (var dbContext = new OneToManyRelationshipDbContext())
      {
        // Loading data.
        var customersWithoutEgaerLoading = dbContext.Customers.ToList();
        Console.WriteLine($"Dump customersWithoutEgaerLoading: {customersWithoutEgaerLoading}");

        // Eager loading include navigation properties
        var customers = dbContext.Customers.Include(blog => blog.Orders).ToList();
        Console.WriteLine($"Egear customers {customers}");
      }

      // Explicit loading Part 1
      using (var dbContext = new OneToManyRelationshipDbContext())
      {
        var customer = dbContext.Customers.Single(b => b.CustomerId == 1);
        Console.WriteLine($"Before Explicit loading: {customer}");

        // Explicit loading
        dbContext.Entry(customer)
          .Collection(b => b.Orders)
          .Load();

        Console.WriteLine($"After Explicit loading: {customer}");
      }

      // Explicit loading Part 2
      using (var dbContext = new OneToManyRelationshipDbContext())
      {
        var order = dbContext.Orders.Single(b => b.OrderId == 1);

        // Explicit loading
        dbContext.Entry(order)
          .Reference(b => b.Customer)
          .Load();
      }

      Console.WriteLine("******************************* ManyToManyRelationship Demo ******************************");
      using (var dbContext = new ManyToManyRelationshipDbContext())
      {
        var customer1 = new Relationships.ManyToMany.Customer
        {
          FirstName = "Bassam",
          LastName = "Alugili"
        };

        var customer2 = new Relationships.ManyToMany.Customer
        {
          FirstName = "Bassam",
          LastName = "Alugili"
        };

        var order1 = new Relationships.ManyToMany.Order
        {
          Description = "Good Product",
          Number = 42
        };

        var order2 = new Relationships.ManyToMany.Order
        {
          Description = "Good Product",
          Number = 42
        };

        var customer1Order1 = new CustomerOrder { Customer = customer1, Order = order1 };
        var customer1Order2 = new CustomerOrder { Customer = customer1, Order = order2 };

        customer1.Orders.Add(customer1Order1);
        customer1.Orders.Add(customer1Order2);

        var customer2Order1 = new CustomerOrder { Customer = customer2, Order = order1 };
        var customer2Order2 = new CustomerOrder { Customer = customer2, Order = order2 };

        customer2.Orders.Add(customer2Order1);
        customer2.Orders.Add(customer2Order2);

        dbContext.Customers.Add(customer1);
        dbContext.Customers.Add(customer2);

        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* TablePerHierarchy Demo ******************************");
      using (var dbContext = new TablePerHierarchyDbContext())
      {
        var customer = new Inheritance.TablePerHierarchy.Customer { FirstName = "Bassam", LastName = "Alugili" };
        var spcialCustomer = new PowerCustomer { FirstName = "Bassam", LastName = "Alugili", Disccount = 50 };

        dbContext.Customers.Add(customer);
        dbContext.PowerCustomers.Add(spcialCustomer);

        dbContext.SaveChanges();
      }

      Console.WriteLine("******************************* ConcurrencyToken Demo ******************************");

      using (var dbContext = new ConcurrencyTokenDbContext())
      {
        var concurrencyTokenUser = new Concurrency.ConcurrencyToken.User { FirstName = "Rami", LastName = "Alugili" };
        dbContext.Users.Add(concurrencyTokenUser);
        dbContext.SaveChanges();
      }

      var concurrencyTokenDbContextFirst = new ConcurrencyTokenDbContext();
      {
        var userConcurrencyTokenFirst = concurrencyTokenDbContextFirst.Users.First(x => x.LastName == "Alugili");
        userConcurrencyTokenFirst.FirstName = "Fadi";
      }

      var concurrencyTokenDbContextSecond = new ConcurrencyTokenDbContext();
      var userConcurrencyTokenSecond = concurrencyTokenDbContextSecond.Users.First(x => x.LastName == "Alugili");
      userConcurrencyTokenSecond.FirstName = "Bassam";

      // Todo see the problem please enable this code
      //concurrencyTokenDbContextFirst.SaveChanges();
      //concurrencyTokenDbContextSecond.SaveChanges();

      Console.WriteLine("******************************* ConcurrencyTimeStamp Demo ******************************");
      using (var dbContext = new ConcurrencyTimeStampDbContext())
      {
        var concurrencyUser = new Concurrency.TimeStamp.User { FirstName = "Rami", LastName = "Alugili" };
        dbContext.Users.Add(concurrencyUser);
        dbContext.SaveChanges();
      }

      var concurrencyDbContextFirst = new ConcurrencyTimeStampDbContext();
      {
        var userConcurrencyFirst = concurrencyDbContextFirst.Users.First(x => x.LastName == "Alugili");
        userConcurrencyFirst.FirstName = "Fadi";
      }

      var concurrencyDbContextSecond = new ConcurrencyTimeStampDbContext();
      var userConcurrencySecond = concurrencyDbContextSecond.Users.First(x => x.LastName == "Alugili");
      userConcurrencySecond.FirstName = "Bassam";

      // Todo see the problem please enable this code
      //concurrencyDbContextFirst.SaveChanges();
      //concurrencyDbContextSecond.SaveChanges();
    }
  }
}