using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreDemo.SimpleExample
{
  public class SimpleExampleDbContext : DbContext
  {
    public DbSet<Blog> Blogs { get; set; }

    public DbSet<Post> Posts { get; set; }

    public SimpleExampleDbContext()
    {
      this.AddSmartInspectLogs();
      this.Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=1_SimpleExampleDatabase;Trusted_Connection=True;");
      // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BloggingDatabase"].ConnectionString);
    }
  }

  public class Blog
  {
    public int BlogId { get; set; }

    public string Url { get; set; }

    public ICollection<Post> Posts { get; set; }
  }

  public class Post
  {
    public int PostId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public int BlogId { get; set; }

    public Blog Blog { get; set; }
  }
}