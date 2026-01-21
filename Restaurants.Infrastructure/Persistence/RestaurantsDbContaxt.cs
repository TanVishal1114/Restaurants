using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence
{
    internal class RestaurantsDbContaxt(DbContextOptions<RestaurantsDbContaxt> options) : IdentityDbContext<UserEntity>(options)
    {
        internal DbSet<Restaurant> Restaurants { get; set; }
        internal DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
