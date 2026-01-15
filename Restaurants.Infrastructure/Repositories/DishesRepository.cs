using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContaxt restaurantsDbContaxt) : IDishesRepository
    {
        public async Task<int> CreateDish(Dish dish)
        {
            restaurantsDbContaxt.Dishes.Add(dish);
            await restaurantsDbContaxt.SaveChangesAsync();
            return dish.Id;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            var dishes=await restaurantsDbContaxt.Dishes.ToListAsync();
            return dishes;
        }
    }
}
