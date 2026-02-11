using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
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
            var restaurants = await dbContaxt.Restaurants.Include(x => x.Dishes).AsNoTracking().ToListAsync();
            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var baseQuery = dbContaxt
            .Restaurants
            .Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
                                                   || r.Description.ToLower().Contains(searchPhraseLower)));
            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
            {
                { nameof(Restaurant.Name), r => r.Name },
                { nameof(Restaurant.Description), r => r.Description },
                { nameof(Restaurant.Category), r => r.Category },
            };

                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }


            var restaurants = await baseQuery
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            // .Include(x => x.Dishes).ToListAsync();
            return (restaurants, totalCount);
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

