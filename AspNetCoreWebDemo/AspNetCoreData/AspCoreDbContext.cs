using System;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreData
{
    public class AspCoreDbContext : DbContext
    {
        public AspCoreDbContext(DbContextOptions<AspCoreDbContext> options)
            : base(options)
        { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
