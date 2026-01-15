using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetByIdForRestaurant
{
    public class GetByIdForRestaurantQueryHandler(ILogger<GetByIdForRestaurantQueryHandler> logger,IRestaurantsRepository restaurantsRepository,IMapper mapper) : IRequestHandler<GetByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishID}, for  restaurant with id: {RestaurantId}",request.DishId,request.RestaurantId);
            var restaurant = await restaurantsRepository.GetRestaurantByIDAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish= restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId) ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());
            return mapper.Map<DishDto>(dish);
        }
    }
}
