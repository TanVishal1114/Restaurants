using MediatR;

namespace Restaurants.Application.Users.Commnds.AssignUserRole
{
    public class AssignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string UserRole { get; set; } = default!;
    }
}
