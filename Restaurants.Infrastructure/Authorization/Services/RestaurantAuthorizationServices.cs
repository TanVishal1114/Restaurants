
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services;

public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
{
    public bool Authorize(Restaurant restaurants, ResourceOperation resourceOperation)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user  {UserEmail}, to {operation} for restaurant {RestaurantName}", user.Email, resourceOperation, restaurants.Name);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation - successful authorization");
            return true;
        }
        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update) && user.Id == restaurants.OwnerId)
        {
            logger.LogInformation("Restaurant owner - successful authorization");
            return true;
        }
        return false;
    }

}
