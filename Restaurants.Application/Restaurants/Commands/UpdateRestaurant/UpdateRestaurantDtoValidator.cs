using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantDtoValidator: AbstractValidator<UpdateRestaurantCommand>   
    {
        public UpdateRestaurantDtoValidator()
        {
            RuleFor(x => x.Name).Length(3, 100);

        }
       
    }
}
