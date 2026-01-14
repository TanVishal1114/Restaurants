using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    internal class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateRestaurantCommand for Restaurant Id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurants = await restaurantsRepository.GetRestaurantByIDAsync(request.Id);
            if (restaurants is null)
            {
                return false;
            }
            _ = mapper.Map(request, restaurants);
            await restaurantsRepository.UpdateRestaurantAsync();
            return true;
        }
    }
}
