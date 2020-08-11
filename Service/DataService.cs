using Microsoft.EntityFrameworkCore;
using Service.Configurations;
using Shared;
namespace Service
{
    public class DataService : DbContext
    {
        public DataService(DbContextOptions<DataService> options) :
            base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Todo>(TodoConfiguration.Create());
            modelBuilder.ApplyConfiguration<User>(UserConfiguration.Create());
        }
    }
}
