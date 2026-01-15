using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContaxt dbContaxt) : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant restaurant)
        {
            await dbContaxt.AddAsync(restaurant);
            await dbContaxt.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContaxt.Restaurants.Include(x=>x.Dishes).AsNoTracking().ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetRestaurantByIDAsync(int id)
        {
            try
            {
                var restaurants = await dbContaxt.Restaurants.Include(r => r.Dishes)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                return restaurants;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            dbContaxt.Remove(restaurant);
            await dbContaxt.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync()
        {
            await dbContaxt.SaveChangesAsync();
        }
    }
}

