using Microsoft.AspNetCore.Identity;

namespace Restaurants.Domain.Entities
{
    public class UserEntity : IdentityUser
    {
        public DateOnly DateofBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
