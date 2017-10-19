using EntityFrameworkCoreDemo.SimpleExample;

namespace EntityFrameworkCoreDemo
{
  public static class DataServiceExtensions
  {
    public static void EnsureSeedData(this SimpleExampleDbContext simpleExampleDbContext)
    {
      var blog = simpleExampleDbContext.Blogs.Add(new Blog { Url = "Seed Data!" }).Entity;
      simpleExampleDbContext.Add(blog);
    }
  }
}