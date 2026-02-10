using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleting restaurant with id: {RestaurantId}", request.Id);
            var restaurantId = await restaurantsRepository.GetRestaurantByIDAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

            if(!restaurantAuthorizationService.Authorize(restaurantId, ResourceOperation.Delete))
            {
                logger.LogWarning("Unauthorized attempt to delete restaurant with id: {RestaurantId}", request.Id);
                throw new ForbidException();
            }
            await restaurantsRepository.DeleteRestaurantAsync(restaurantId);
        }
    }
}
