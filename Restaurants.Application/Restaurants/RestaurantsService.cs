using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Creating a new restaurant");
        var restaurant=mapper.Map<Restaurant>(createRestaurantDto);
        int id=await restaurantsRepository.Create(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restauranDto = await restaurantsRepository.GetAllAsync();
        var restauran = mapper.Map<IEnumerable<RestaurantDto>>(restauranDto);
        return restauran;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation("Getting restaurant by Id");
        var restauranDto = await restaurantsRepository.GetRestaurantByIDAsync(id);
        var restauran = mapper.Map<RestaurantDto?>(restauranDto);
        return restauran;
    }
}
