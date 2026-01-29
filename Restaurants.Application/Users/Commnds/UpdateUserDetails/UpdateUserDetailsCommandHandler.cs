using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;


namespace Restaurants.Application.Users.Commnds.UpdateUserRoles
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger, IUserContext userContext, IUserStore<UserEntity> userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating User: {UserId}, with {@Request}", user!.Id, request);
            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken)?? throw new NotFoundException(nameof(User),user!.Id);
            dbUser.Nationality = request.Nationality;
            dbUser.DateofBirth = request.DateOfBirth ?? dbUser.DateofBirth; 
            await userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
