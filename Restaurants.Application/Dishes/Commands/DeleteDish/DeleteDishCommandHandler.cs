using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler(ILogger<DeleteDishCommandHandler> logger, IDishesRepository dishesRepository, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteDishCommand>
    {
        public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting dishes for restaurant {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetRestaurantByIDAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            //var dish=restaurantId.Dishes.Where(d => d.Id == request.DishId).FirstOrDefault() ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            await dishesRepository.DeleteDishAsync(restaurant.Dishes);
        }
    }
}
