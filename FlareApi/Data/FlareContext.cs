using FlareApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlareApi.Data
{
    public class FlareContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Session> Sessions { get; set; } = null!;

        public FlareContext(DbContextOptions options) : base(options)
        {
        }
    }
}