using MediatR;

namespace Restaurants.Application.Users.Commnds.UpdateUserRoles

{
    public class UpdateUserDetailsCommand:IRequest
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
