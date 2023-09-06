using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Middleware
{
    public class RickAndMortyContext : DbContext
    {
        public virtual DbSet<Location> Locations { get; set; }
        public RickAndMortyContext(DbContextOptions<RickAndMortyContext> options): base(options)
        { 
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLowerCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");
                entity.ToTable("Location");
            });
        }
    }
}
