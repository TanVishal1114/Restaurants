using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    internal class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
    {
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurants = await restaurantsRepository.GetRestaurantByIDAsync(request.Id)??  throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            _ = mapper.Map(request, restaurants);

            if (!restaurantAuthorizationService.Authorize(restaurants, ResourceOperation.Update))
            {
                throw new ForbidException();
            }
            await restaurantsRepository.UpdateRestaurantAsync();
        }
    }
}
