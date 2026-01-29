using MediatR;

namespace Restaurants.Application.Users.Commnds.RemoveUserRole
{
    public class RemoveUserRoleCommand:IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string UserRole { get; set; } = default!;
    }
}
