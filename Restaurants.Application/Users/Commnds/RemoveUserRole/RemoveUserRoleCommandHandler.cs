using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Application.Users.Commnds.AssignUserRole;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commnds.RemoveUserRole
{
    public class RemoveUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserRoleCommand>
    {
        public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Unassigning user role: {@Request}", request);
            var user = await userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundException(nameof(CurrentUser), request.UserEmail);
            var role = await roleManager.FindByNameAsync(request.UserRole) ?? throw new NotFoundException(nameof(CurrentUser), request.UserRole);
            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
