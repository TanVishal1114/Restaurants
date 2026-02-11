using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler(ILogger<CreatedMultipleRestaurantsRequirementHandler> logger, IUserContext userContext, IRestaurantsRepository restaurantsRepository) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();   

            var restaurants = await restaurantsRepository.GetAllAsync();

            var userRestaurantCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);

            if(userRestaurantCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);

            }
            else
            {
                logger.LogWarning("User {UserId} has only created {UserRestaurantCreated} restaurants, which is less than the required {MinimumRestaurantsCreated} to meet the requirement.", currentUser.Id, userRestaurantCreated, requirement.MinimumRestaurantsCreated);
                context.Fail(); 
            }
        }

        
    }
}
