using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetRestaurantByIDAsync(int id);
        Task<int> Create(Restaurant restaurant);
        Task DeleteRestaurantAsync(Restaurant restaurant);
        Task UpdateRestaurantAsync();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,int pageSize, int pageNumber);
    }
}
