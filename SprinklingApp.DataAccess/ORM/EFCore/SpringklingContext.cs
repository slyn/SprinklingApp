using Microsoft.EntityFrameworkCore;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.DataAccess.ORM.EFCore
{
    public class SpringklingContext : DbContext
    {
        public SpringklingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Valve> Valve { get; set; }
        public DbSet<Raspberry> Raspberry { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<ProfileGroupMapping> ProfileGroupMapping { get; set; }
        public DbSet<ValveGroupMapping> ValveGroupMapping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed data
        }
    }
}
