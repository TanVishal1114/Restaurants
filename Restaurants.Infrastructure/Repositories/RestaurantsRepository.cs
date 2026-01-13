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
            var restaurants = await dbContaxt.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetRestaurantByIDAsync(int id)
        {
            var restaurants = await dbContaxt.Restaurants.FirstOrDefaultAsync(x => x.Id == id);
            return restaurants;
        }

        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            dbContaxt.Remove(restaurant);
            await dbContaxt.SaveChangesAsync();
        }
    }
}

