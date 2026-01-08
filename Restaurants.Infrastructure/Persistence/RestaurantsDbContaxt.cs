using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContaxt(DbContextOptions<RestaurantsDbContaxt> options) : DbContext(options)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=RestaurantDb;Trusted_Connection=True;Encrypt=False;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.OwnsOne(r => r.Address);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasMany(r => r.Dishes)
                      .WithOne()
                      .HasForeignKey(d => d.RestaurantId);
            });
        }
    }
}
