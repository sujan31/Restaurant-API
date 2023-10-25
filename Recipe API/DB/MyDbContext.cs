using Microsoft.EntityFrameworkCore;
using Recipe_API.Model;

namespace Recipe_API.DB
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Recipe> Recipes { get; set; }
    }
}
