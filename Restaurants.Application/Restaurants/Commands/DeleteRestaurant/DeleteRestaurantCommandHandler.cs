using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id: {RestaurantId}", request.Id);
            var restaurantId = await restaurantsRepository.GetRestaurantByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            await restaurantsRepository.DeleteRestaurantAsync(restaurantId);
        }
    }
}
