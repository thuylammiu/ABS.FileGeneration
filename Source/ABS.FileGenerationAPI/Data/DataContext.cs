using ABS.FileGenerationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ABS.FileGenerationAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasData(
                    new AppUser {  Id =1, Password="password", Role = "admin", UserName="admin"},
                    new AppUser { Id = 2, Password = "password", Role = "user", UserName = "user" }
                );
        }
    }
}
