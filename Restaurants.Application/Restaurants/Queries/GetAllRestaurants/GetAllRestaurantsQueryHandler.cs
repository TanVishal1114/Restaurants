using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PageResult<RestaurantDto>>
    {
        public async Task<PageResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restauranDto, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase, request.PageSize, request.PageNumber);
            var restauran = mapper.Map<IEnumerable<RestaurantDto>>(restauranDto);

            var result = new PageResult<RestaurantDto>(restauran, totalCount, request.PageSize, request.PageNumber);

            return result;
        }

    }
}
