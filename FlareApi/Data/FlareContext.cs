using FlareApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlareApi.Data
{
    public class FlareContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;

        public FlareContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(u => u.Age)
                .HasDefaultValue(18);
        }
    }
}