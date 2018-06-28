using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApiData
{
    public class ApiDbcontext: DbContext
    {
        public ApiDbcontext(DbContextOptions<ApiDbcontext> options): base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<ConfigurationValue> ConfigurationValues { get; set; }
    }
}
