using ActorsWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ActorsWebApplication.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ActorsDb");
        }
        public DbSet<ActorModel> Actors { get; set; }
    }
}
