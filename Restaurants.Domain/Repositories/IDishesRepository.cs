using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<int> CreateDish(Dish dish);
        Task DeleteDishAsync(IEnumerable<Dish> dish);
    }
}
